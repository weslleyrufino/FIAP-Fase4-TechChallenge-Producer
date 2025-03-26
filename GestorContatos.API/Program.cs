using GestorContatos.API.Logging;
using GestorContatos.Application.Interfaces.Repository;
using GestorContatos.Application.Interfaces.Services;
using GestorContatos.Application.Services;
using GestorContatos.Infrastructure.Repository;
using MassTransit;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Prometheus;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

var httpDuration = Metrics.CreateHistogram("http_request_duration_seconds_sum", "Histogram of HTTP request durations.", new HistogramConfiguration
{
    LabelNames = new[] { "method", "endpoint" }
});

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddScoped<IContatoRepository, ContatoRepository>();
builder.Services.AddScoped<IContatoService, ContatoService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var configurationMassTransit = builder.Configuration;
var fila = configurationMassTransit.GetSection("MassTransit")["NomeFila"] ?? string.Empty;
var servidor = configurationMassTransit.GetSection("MassTransit")["Servidor"] ?? string.Empty;
var usuario = configurationMassTransit.GetSection("MassTransit")["Usuario"] ?? string.Empty;
var senha = configurationMassTransit.GetSection("MassTransit")["Senha"] ?? string.Empty;

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(servidor, "/", h =>
        {
            h.Username(usuario);
            h.Password(senha);
        });

        cfg.ConfigureEndpoints(context);
    });
});

// LOG
builder.Logging.ClearProviders();
builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration
{
    LogLevel = LogLevel.Information,
}));
//LOG

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("ConnectionString"));
}, ServiceLifetime.Scoped);

var app = builder.Build();

// Registra métricas de uso de CPU e memória
var cpuUsage = Metrics.CreateGauge("system_cpu_usage_percent", "Current CPU usage percentage.");
var memoryUsage = Metrics.CreateGauge("system_memory_usage_bytes", "Current memory usage in bytes.");

// Variáveis para cálculo do uso de CPU
var lastTotalProcessorTime = TimeSpan.Zero;
var lastTime = DateTime.UtcNow;


// Middleware para coletar métricas de uso de CPU e memória
app.Use(async (context, next) =>
{
    var process = Process.GetCurrentProcess();

    // Cálculo do uso de CPU (percentual)
    var currentTime = DateTime.UtcNow;
    var currentTotalProcessorTime = process.TotalProcessorTime;

    var elapsedTime = (currentTime - lastTime).TotalMilliseconds;
    var cpuElapsedTime = (currentTotalProcessorTime - lastTotalProcessorTime).TotalMilliseconds;

    var cpuPercent = elapsedTime > 0 ? (cpuElapsedTime / elapsedTime) * 100 / Environment.ProcessorCount : 0;

    // Atualiza valores para o próximo cálculo
    lastTime = currentTime;
    lastTotalProcessorTime = currentTotalProcessorTime;

    // Define a métrica de CPU
    cpuUsage.Set(cpuPercent);

    // Cálculo do uso de memória (percentual)
    var totalMemory = GC.GetGCMemoryInfo().TotalAvailableMemoryBytes; // Total de memória disponível para o processo
    var usedMemory = process.PrivateMemorySize64; // Memória usada pelo processo

    var memoryPercent = totalMemory > 0 ? (usedMemory / (double)totalMemory) * 100 : 0;

    // Define a métrica de memória
    memoryUsage.Set(memoryPercent);

    await next();
});


// Middleware para medir latência das requisições
app.Use(async (context, next) =>
{
    var stopwatch = System.Diagnostics.Stopwatch.StartNew();
    await next();
    stopwatch.Stop();
    httpDuration
        .WithLabels(context.Request.Method, context.Request.Path)
        .Observe(stopwatch.Elapsed.TotalSeconds);
});

// Middleware de tratamento de exceções
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();

        if (exceptionHandlerPathFeature?.Error != null)
        {
            logger.LogError(exceptionHandlerPathFeature.Error, "Erro não tratado.");
        }

        await context.Response.WriteAsync("{\"error\":\"Erro interno do servidor.\"}");
    });
});

// Middleware padrão

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.UseHttpMetrics();

app.MapMetrics();

app.MapControllers();

app.Run();

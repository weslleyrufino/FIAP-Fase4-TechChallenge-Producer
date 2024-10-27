using GestorContatos.Core.Interfaces.Repository;
using GestorContatos.Core.Interfaces.Services;
using GestorContatos.Core.Services;
using GestorContatos.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IContatoRepository, ContatoRepository>();
builder.Services.AddScoped<IContatoService, ContatoService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

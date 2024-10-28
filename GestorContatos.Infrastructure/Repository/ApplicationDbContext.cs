using GestorContatos.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestorContatos.Infrastructure.Repository;
public class ApplicationDbContext : DbContext
{
    private readonly string _connectionString;

    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public DbSet<ContatoModel> Contato { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlServer(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}

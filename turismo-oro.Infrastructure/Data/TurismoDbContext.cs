using Microsoft.EntityFrameworkCore;
using turismo_oro.Domain.Entities;

namespace turismo_oro.Infrastructure.Data;

public class TurismoDbContext : DbContext
{
    public TurismoDbContext(DbContextOptions<TurismoDbContext> options) : base(options)
    {
    }

    public DbSet<TurismoLugar> TurismoLugares => Set<TurismoLugar>();
    public DbSet<Archivo> Archivos => Set<Archivo>();
    public DbSet<TurismoArchivo> TurismoArchivos => Set<TurismoArchivo>();
    public DbSet<ComentarioTurismo> ComentariosTurismo => Set<ComentarioTurismo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TurismoDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}

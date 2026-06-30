using Microsoft.EntityFrameworkCore;
using turismo_oro.Domain.Entities;

namespace turismo_oro.Infrastructure.Data;

public class SqlDbContext : DbContext
{
    public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
    {
    }

    public DbSet<TurismoLugar> TurismoLugares => Set<TurismoLugar>();
    public DbSet<Archivo> Archivos => Set<Archivo>();
    public DbSet<TurismoArchivo> TurismoArchivos => Set<TurismoArchivo>();
    public DbSet<ComentarioTurismo> ComentariosTurismo => Set<ComentarioTurismo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Archivo>()
            .Property(a => a.TipoArchivo)
            .HasConversion<string>();

        base.OnModelCreating(modelBuilder);
    }
}

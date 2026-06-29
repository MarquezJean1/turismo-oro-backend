using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using turismo_oro.Domain.Entities;

namespace turismo_oro.Infrastructure.Data.Configurations;

public class TurismoLugarConfiguration : IEntityTypeConfiguration<TurismoLugar>
{
    public void Configure(EntityTypeBuilder<TurismoLugar> builder)
    {
        builder.ToTable("TurismoLugares");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Nombre)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(t => t.Categoria)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(t => t.Latitud)
            .HasPrecision(10, 7)
            .IsRequired();

        builder.Property(t => t.Longitud)
            .HasPrecision(10, 7)
            .IsRequired();

        builder.Property(t => t.Direccion)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(t => t.EtiquetaPrecio)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(t => t.Descripcion)
            .HasMaxLength(4000)
            .IsRequired();

        builder.Property(t => t.Destacados)
            .HasMaxLength(1000);

        builder.Property(t => t.FechaCreacion)
            .IsRequired();

        builder.HasMany(t => t.TurismoArchivos)
            .WithOne(ta => ta.TurismoLugar)
            .HasForeignKey(ta => ta.TurismoLugarId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.Comentarios)
            .WithOne(c => c.TurismoLugar)
            .HasForeignKey(c => c.TurismoLugarId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

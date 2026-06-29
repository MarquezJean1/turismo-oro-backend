using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using turismo_oro.Domain.Entities;
using turismo_oro.Domain.Enums;

namespace turismo_oro.Infrastructure.Data.Configurations;

public class ArchivoConfiguration : IEntityTypeConfiguration<Archivo>
{
    public void Configure(EntityTypeBuilder<Archivo> builder)
    {
        builder.ToTable("Archivos");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Url)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(a => a.Nombre)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(a => a.Extension)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(a => a.TipoArchivo)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(a => a.FechaCreacion)
            .IsRequired();
    }
}

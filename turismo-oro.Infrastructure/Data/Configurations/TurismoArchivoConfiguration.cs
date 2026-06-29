using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using turismo_oro.Domain.Entities;

namespace turismo_oro.Infrastructure.Data.Configurations;

public class TurismoArchivoConfiguration : IEntityTypeConfiguration<TurismoArchivo>
{
    public void Configure(EntityTypeBuilder<TurismoArchivo> builder)
    {
        builder.ToTable("TurismoArchivos");

        builder.HasKey(ta => new { ta.TurismoLugarId, ta.ArchivoId });

        builder.Property(ta => ta.Orden)
            .IsRequired();

        builder.HasOne(ta => ta.Archivo)
            .WithMany(a => a.TurismoArchivos)
            .HasForeignKey(ta => ta.ArchivoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

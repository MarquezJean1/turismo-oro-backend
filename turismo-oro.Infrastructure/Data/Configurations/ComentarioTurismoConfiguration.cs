using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using turismo_oro.Domain.Entities;

namespace turismo_oro.Infrastructure.Data.Configurations;

public class ComentarioTurismoConfiguration : IEntityTypeConfiguration<ComentarioTurismo>
{
    public void Configure(EntityTypeBuilder<ComentarioTurismo> builder)
    {
        builder.ToTable("ComentariosTurismo");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Autor)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(c => c.Calificacion)
            .IsRequired();

        builder.Property(c => c.Texto)
            .HasMaxLength(2000)
            .IsRequired();

        builder.Property(c => c.FechaCreacion)
            .IsRequired();
    }
}

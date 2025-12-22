using BackendPTDetecta.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendPTDetecta.Infrastructure.Persistence.Configurations;

public class TipoSeguroConfiguration : IEntityTypeConfiguration<TipoSeguro>
{
    public void Configure(EntityTypeBuilder<TipoSeguro> builder)
    {
        builder.ToTable("TIPOS_SEGUROS");

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).HasColumnName("NU_ID_TIPO_SEGURO");
        
        builder.Property(t => t.Nombre)
            .HasColumnName("TX_NOM_SEGURO")
            .HasMaxLength(50)
            .IsRequired();

        // SEEDING (Datos Semilla)
        builder.HasData(
            new TipoSeguro { Id = 1, Nombre = "SIS" },
            new TipoSeguro { Id = 2, Nombre = "EsSalud" },
            new TipoSeguro { Id = 3, Nombre = "EPS Pacífico" },
            new TipoSeguro { Id = 4, Nombre = "Rimac Seguros" },
            new TipoSeguro { Id = 5, Nombre = "Particular" }
        );
    }
}
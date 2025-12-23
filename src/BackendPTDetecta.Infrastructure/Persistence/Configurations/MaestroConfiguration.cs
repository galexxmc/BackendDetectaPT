using BackendPTDetecta.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendPTDetecta.Infrastructure.Persistence.Configurations;

public class MaestroConfiguration : IEntityTypeConfiguration<Maestro>
{
    public void Configure(EntityTypeBuilder<Maestro> builder)
    {
        builder.ToTable("MAESTROS");

        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id).HasColumnName("NU_ID_MAESTRO");

        builder.Property(m => m.Grupo)
            .HasColumnName("TX_GRUPO") 
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(m => m.Nombre)
            .HasColumnName("TX_NOMBRE")
            .HasMaxLength(100)
            .IsRequired();

        // Campos de Auditoría (Mapeo estándar)
        builder.Property(m => m.EstadoRegistro).HasColumnName("NU_ESTADO");
        builder.Property(m => m.UsuarioRegistro).HasColumnName("TX_USU_REG");
        builder.Property(m => m.FechaRegistro).HasColumnName("FE_REG");
        builder.Property(m => m.UsuarioModificacion).HasColumnName("TX_USU_MOD");
        builder.Property(m => m.FechaModificacion).HasColumnName("FE_MOD");
        // ... mapea los de eliminación si los usas ...

        // 🔥 SEEDING: Aquí definimos los IDs FIJOS.
        // Esto valida tu lógica: "El sistema sabrá que el 1 es Masculino".
        builder.HasData(
            // --- GRUPO: SEXO (IDs 1 y 2) ---
            new Maestro { Id = 1, Grupo = "SEXO", Nombre = "Masculino", EstadoRegistro = 1, FechaRegistro = DateTime.UtcNow, UsuarioRegistro = "SYSTEM" },
            new Maestro { Id = 2, Grupo = "SEXO", Nombre = "Femenino", EstadoRegistro = 1, FechaRegistro = DateTime.UtcNow, UsuarioRegistro = "SYSTEM" },

            // --- GRUPO: SEGURO (IDs 3 al 7) ---
            new Maestro { Id = 3, Grupo = "SEGURO", Nombre = "SIS", EstadoRegistro = 1, FechaRegistro = DateTime.UtcNow, UsuarioRegistro = "SYSTEM" },
            new Maestro { Id = 4, Grupo = "SEGURO", Nombre = "EsSalud", EstadoRegistro = 1, FechaRegistro = DateTime.UtcNow, UsuarioRegistro = "SYSTEM" },
            new Maestro { Id = 5, Grupo = "SEGURO", Nombre = "EPS Pacífico", EstadoRegistro = 1, FechaRegistro = DateTime.UtcNow, UsuarioRegistro = "SYSTEM" },
            new Maestro { Id = 6, Grupo = "SEGURO", Nombre = "Rimac Seguros", EstadoRegistro = 1, FechaRegistro = DateTime.UtcNow, UsuarioRegistro = "SYSTEM" },
            new Maestro { Id = 7, Grupo = "SEGURO", Nombre = "Particular", EstadoRegistro = 1, FechaRegistro = DateTime.UtcNow, UsuarioRegistro = "SYSTEM" }
        );
    }
}
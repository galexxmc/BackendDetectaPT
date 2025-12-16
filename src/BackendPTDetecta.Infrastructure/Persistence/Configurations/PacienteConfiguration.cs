using BackendPTDetecta.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendPTDetecta.Infrastructure.Persistence.Configurations
{
    public class PacienteConfiguration : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.ToTable("PACIENTES");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("NU_ID_PACIENTE");

            builder.Property(p => p.Nombres)
                .HasColumnName("TX_NOM_PACIEN")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Apellidos)
                .HasColumnName("TX_APE_PACIEN")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Dni)
                .HasColumnName("NU_DNI_PACIEN")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(p => p.Codigo)
                .HasColumnName("TX_CODIGO_PACIENTE")
                .ValueGeneratedOnAddOrUpdate(); 

            builder.Property(p => p.FechaNacimiento)
                .HasColumnName("FE_NACIMIENTO")
                .HasColumnType("date");

            builder.Property(p => p.Sexo)
                .HasColumnName("NU_ID_SEXO")
                .IsRequired();

            builder.Property(p => p.UsuarioRegistro).HasColumnName("TX_USU_REG");
            builder.Property(p => p.FechaRegistro).HasColumnName("FE_REG");
            builder.Property(p => p.EstadoRegistro).HasColumnName("NU_ESTADO"); 
        }
    }
}
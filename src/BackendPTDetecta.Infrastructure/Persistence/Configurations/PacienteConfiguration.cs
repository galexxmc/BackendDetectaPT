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
                .HasMaxLength(20);

            builder.Property(p => p.FechaNacimiento)
                .HasColumnName("FE_NACIMIENTO")
                .HasColumnType("date");

            builder.Property(p => p.Sexo)
                .HasColumnName("NU_ID_SEXO")
                .IsRequired();

            builder.Property(p => p.Direccion)
                .HasColumnName("TX_DIRECCION")
                .HasMaxLength(200);

            builder.Property(p => p.Telefono)
                .HasColumnName("TX_TELEFONO")
                .HasMaxLength(20);

            builder.Property(p => p.Email)
                .HasColumnName("TX_EMAIL")
                .HasMaxLength(100);

            builder.Property(p => p.UsuarioRegistro)
                .HasColumnName("TX_USU_REG")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.FechaRegistro)
                .HasColumnName("FE_REG")
                .HasColumnType("datetime2(0)")
                .IsRequired();

            builder.Property(p => p.UsuarioModificacion)
                .HasColumnName("TX_USU_MOD")
                .HasMaxLength(50);

            builder.Property(p => p.FechaModificacion)
                .HasColumnName("FE_MOD")
                .HasColumnType("datetime2(0)");

            builder.Property(p => p.UsuarioEliminacion)
                .HasColumnName("TX_USU_ELI")
                .HasMaxLength(50);

            builder.Property(p => p.FechaEliminacion)
                .HasColumnName("FE_ELI")
                .HasColumnType("datetime2(0)");             

            builder.Property(p => p.MotivoEliminacion)
                .HasColumnName("TX_MOTIVO_ELI")
                .HasMaxLength(500);

            builder.Property(p => p.EstadoRegistro)
                .HasColumnName("NU_ESTADO");

            builder.Property(p => p.TipoSeguroId)
                .HasColumnName("NU_ID_TIPO_SEGURO"); 

            builder.HasOne(p => p.TipoSeguro)
                .WithMany()
                .HasForeignKey(p => p.TipoSeguroId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
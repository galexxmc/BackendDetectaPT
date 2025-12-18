using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BackendPTDetecta.Application.Common.Interfaces;
using BackendPTDetecta.Domain.Common;
using BackendPTDetecta.Domain.Entities;
using BackendPTDetecta.Infrastructure.Identity;

namespace BackendPTDetecta.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    private readonly ICurrentUserService _currentUserService; // <--- 1. Variable nueva

    // Inyectamos el servicio en el constructor
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        ICurrentUserService currentUserService) : base(options)
    {
        _currentUserService = currentUserService;
    }

    public DbSet<Paciente> Pacientes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    // --- 2. LA MAGIA DE LA AUDITORÍA ---
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var usuarioActual = _currentUserService.CodigoUsuario ?? "ANONIMO";
        var ahoraPeru = DateTime.UtcNow.AddHours(-5);

        foreach (var entry in ChangeTracker.Entries<EntidadAuditable>())
        {
            // SI ES NUEVO (ID = 0)
            if (entry.State == EntityState.Added || entry.Entity.Id == 0)
            {
                entry.State = EntityState.Added;
                entry.Entity.UsuarioRegistro = usuarioActual;
                entry.Entity.FechaRegistro = ahoraPeru;
                entry.Entity.EstadoRegistro = 1;

                // Registro limpio: Nada de modificación
                entry.Entity.UsuarioModificacion = null;
                entry.Entity.FechaModificacion = null;
            }
            // SI ES MODIFICACIÓN
            else if (entry.State == EntityState.Modified)
            {
                // 1. Detectar Soft Delete (Ya lo tenías)
                var esSoftDelete = entry.Property(x => x.FechaEliminacion).IsModified
                                   && entry.Entity.FechaEliminacion != null;

                // 2. LA TRAMPA: Detectar Generación de Código Automática 🕸️
                // Lógica: Si la propiedad "Codigo" existe y fue modificada...
                // ... Y la fecha de modificación original era NULL (o sea, nadie lo había editado antes)...
                // ... Entonces asumimos que es el proceso de creación automática.
                var esGeneracionCodigo = false;

                // Verificamos si la entidad tiene la propiedad "Codigo" (porque EntidadAuditable no la tiene, Paciente sí)
                var propiedadCodigo = entry.Properties.FirstOrDefault(p => p.Metadata.Name == "Codigo");

                if (propiedadCodigo != null && propiedadCodigo.IsModified)
                {
                    // Si nunca antes fue modificado (es null en la BD), entonces es el segundo guardado del registro.
                    if (entry.OriginalValues["FechaModificacion"] == null)
                    {
                        esGeneracionCodigo = true;
                    }
                }

                // SOLO actualizamos auditoría si NO es SoftDelete Y NO es Generación de Código
                if (!esSoftDelete && !esGeneracionCodigo)
                {
                    entry.Entity.UsuarioModificacion = usuarioActual;
                    entry.Entity.FechaModificacion = ahoraPeru;
                }

                // Protegemos los datos de registro para que no cambien
                entry.Property(x => x.UsuarioRegistro).IsModified = false;
                entry.Property(x => x.FechaRegistro).IsModified = false;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
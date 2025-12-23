using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BackendPTDetecta.Application.Common.Interfaces;
using BackendPTDetecta.Domain.Common;
using BackendPTDetecta.Domain.Entities;
using BackendPTDetecta.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BackendPTDetecta.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    private readonly ICurrentUserService _currentUserService;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        ICurrentUserService currentUserService) : base(options)
    {
        _currentUserService = currentUserService;
    }

    // Tabla de Pacientes (Ya existía)
    public DbSet<Paciente> Pacientes { get; set; }
    public DbSet<Maestro> Maestros => Set<Maestro>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ConfigureWarnings(warnings => 
            warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
            
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Esto busca tu TipoSeguroConfiguration automáticamente
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    // --- LÓGICA DE AUDITORÍA (SE MANTIENE IGUAL) ---
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var usuarioActual = _currentUserService.CodigoUsuario ?? "ANONIMO";
        var ahoraPeru = DateTime.UtcNow.AddHours(-5);

        foreach (var entry in ChangeTracker.Entries<EntidadAuditable>())
        {
            if (entry.State == EntityState.Added || entry.Entity.Id == 0)
            {
                entry.State = EntityState.Added;
                entry.Entity.UsuarioRegistro = usuarioActual;
                entry.Entity.FechaRegistro = ahoraPeru;
                entry.Entity.EstadoRegistro = 1;
                entry.Entity.UsuarioModificacion = null;
                entry.Entity.FechaModificacion = null;
            }
            else if (entry.State == EntityState.Modified)
            {
                var esSoftDelete = entry.Property(x => x.FechaEliminacion).IsModified
                                   && entry.Entity.FechaEliminacion != null;

                var esGeneracionCodigo = false;
                var propiedadCodigo = entry.Properties.FirstOrDefault(p => p.Metadata.Name == "Codigo");

                if (propiedadCodigo != null && propiedadCodigo.IsModified)
                {
                    if (entry.OriginalValues["FechaModificacion"] == null)
                    {
                        esGeneracionCodigo = true;
                    }
                }

                if (!esSoftDelete && !esGeneracionCodigo)
                {
                    entry.Entity.UsuarioModificacion = usuarioActual;
                    entry.Entity.FechaModificacion = ahoraPeru;
                }

                entry.Property(x => x.UsuarioRegistro).IsModified = false;
                entry.Property(x => x.FechaRegistro).IsModified = false;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
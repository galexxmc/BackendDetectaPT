using Microsoft.EntityFrameworkCore;
using BackendPTDetecta.Domain.Entities;

namespace BackendPTDetecta.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    // Aquí definimos qué tablas necesita ver la aplicación
    DbSet<Paciente> Pacientes { get; }

    // Y el método para guardar cambios
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
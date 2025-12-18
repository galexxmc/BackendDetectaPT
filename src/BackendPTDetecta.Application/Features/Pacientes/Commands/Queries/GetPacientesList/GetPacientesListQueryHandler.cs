using MediatR;
using Microsoft.EntityFrameworkCore;
using BackendPTDetecta.Application.Common.Interfaces;
using BackendPTDetecta.Application.Common.Models;

namespace BackendPTDetecta.Application.Features.Pacientes.Queries.GetPacientesList;

public class GetPacientesListQueryHandler : IRequestHandler<GetPacientesListQuery, PaginatedList<PacienteDto>>
{
    private readonly IApplicationDbContext _context;

    public GetPacientesListQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<PacienteDto>> Handle(GetPacientesListQuery request, CancellationToken cancellationToken)
    {
        // 1. Iniciamos la consulta base (solo activos)
        var query = _context.Pacientes
            .Where(p => p.FechaEliminacion == null)
            .AsQueryable(); // Importante para seguir agregando condiciones

        // 2. FILTRO INTELIGENTE 🔍
        // Si el usuario escribió algo, filtramos por Nombre, Apellido O DNI
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
                {
                    string texto = request.SearchTerm.Trim(); // Quitamos espacios vacíos
                    // EF Core traducirá esto a SQL con "LIKE"
                    query = query.Where(p => 
                        p.Nombres.Contains(texto) || 
                        p.Apellidos.Contains(texto) || 
                        p.Dni.Contains(texto)
                    );
                }

        // 3. Ordenamos y Proyectamos (Igual que antes)
        var dtoQuery = query
            .OrderByDescending(p => p.Id)
            .Select(p => new PacienteDto
            {
                Id = p.Id,
                Codigo = p.Codigo,
                Nombres = p.Nombres,
                Apellidos = p.Apellidos,
                Dni = p.Dni,
                FechaNacimiento = p.FechaNacimiento,
                Edad = p.Edad,
                Sexo = p.Sexo.ToString(),
                Telefono = p.Telefono,
                Email = p.Email
            });
        // 2. Usamos nuestra herramienta para Paginar y Ejecutar
        // Esto hace el Skip, Take, cuenta el total y devuelve la lista lista.
        return await PaginatedList<PacienteDto>
                    .CreateAsync(dtoQuery, request.PageNumber, request.PageSize);
    }
}
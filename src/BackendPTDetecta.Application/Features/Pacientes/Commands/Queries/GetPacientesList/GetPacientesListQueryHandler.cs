using MediatR;
using Microsoft.EntityFrameworkCore;
using BackendPTDetecta.Application.Common.Interfaces; // Usamos la Interfaz

namespace BackendPTDetecta.Application.Features.Pacientes.Queries.GetPacientesList;

public class GetPacientesListQueryHandler : IRequestHandler<GetPacientesListQuery, List<PacienteDto>>
{
    private readonly IApplicationDbContext _context; // Interfaz

    public GetPacientesListQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<PacienteDto>> Handle(GetPacientesListQuery request, CancellationToken cancellationToken)
    {
        return await _context.Pacientes
            .Where(p => p.FechaEliminacion == null)
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
            })
            .ToListAsync(cancellationToken);
    }
}
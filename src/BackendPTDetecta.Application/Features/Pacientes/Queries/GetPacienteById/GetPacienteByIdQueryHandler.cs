using MediatR;
using Microsoft.EntityFrameworkCore;
using BackendPTDetecta.Application.Common.Interfaces;

namespace BackendPTDetecta.Application.Features.Pacientes.Queries.GetPacienteById;

public class GetPacienteByIdQueryHandler : IRequestHandler<GetPacienteByIdQuery, PacienteDetalleDto?>
{
    private readonly IApplicationDbContext _context;

    public GetPacienteByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PacienteDetalleDto?> Handle(GetPacienteByIdQuery request, CancellationToken cancellationToken)
    {
        var paciente = await _context.Pacientes
            .Where(p => p.Id == request.Id && p.FechaEliminacion == null) // Filtramos por ID y que no esté eliminado
            .FirstOrDefaultAsync(cancellationToken);

        // Si no existe, devolvemos null (el controlador se encargará de decir "404 Not Found")
        if (paciente == null) return null;

        // Mapeamos a mano (Convertimos Entidad -> DTO)
        return new PacienteDetalleDto
        {
            Id = paciente.Id,
            Codigo = paciente.Codigo,
            Nombres = paciente.Nombres,
            Apellidos = paciente.Apellidos,
            Dni = paciente.Dni,
            FechaNacimiento = paciente.FechaNacimiento,
            Edad = paciente.Edad,
            Sexo = paciente.Sexo != null ? paciente.Sexo.Nombre : "Desconocido",
            Telefono = paciente.Telefono,
            Email = paciente.Email,
            Direccion = paciente.Direccion,
            NombreSeguro = paciente.TipoSeguro != null ? paciente.TipoSeguro.Nombre : "Particular",
        };
    }
}
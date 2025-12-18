using MediatR;
using BackendPTDetecta.Application.Common.Interfaces;

namespace BackendPTDetecta.Application.Features.Pacientes.Commands.ActualizarPaciente;

public class ActualizarPacienteCommandHandler : IRequestHandler<ActualizarPacienteCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public ActualizarPacienteCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(ActualizarPacienteCommand request, CancellationToken cancellationToken)
    {
        // 1. BUSCAR: Intentamos encontrar al paciente por su ID
        var entity = await _context.Pacientes
            .FindAsync(new object[] { request.Id }, cancellationToken);

        // 2. VALIDAR EXISTENCIA: Si no existe o fue eliminado, lanzamos error
        if (entity == null || entity.FechaEliminacion != null)
        {
            throw new KeyNotFoundException($"No se encontró el paciente con ID {request.Id}");
        }

        // 3. ACTUALIZAR CAMPOS: Pasamos los datos nuevos a la entidad
        entity.Nombres = request.Nombres;
        entity.Apellidos = request.Apellidos;
        entity.Dni = request.Dni;
        entity.FechaNacimiento = request.FechaNacimiento;
        entity.Sexo = (Domain.Enums.Sexo)request.SexoId;
        entity.Direccion = request.Direccion ?? string.Empty;
        entity.Telefono = request.Telefono ?? string.Empty;
        entity.Email = request.Email ?? string.Empty;

        // 5. GUARDAR
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value; // Retorno vacío (Éxito)
    }
}
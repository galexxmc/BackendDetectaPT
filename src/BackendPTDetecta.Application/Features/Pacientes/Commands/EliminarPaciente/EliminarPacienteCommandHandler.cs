using MediatR;
using BackendPTDetecta.Application.Common.Interfaces;

namespace BackendPTDetecta.Application.Features.Pacientes.Commands.EliminarPaciente;

public class EliminarPacienteCommandHandler : IRequestHandler<EliminarPacienteCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public EliminarPacienteCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(EliminarPacienteCommand request, CancellationToken cancellationToken)
    {
        // 1. Buscar
        var entity = await _context.Pacientes
            .FindAsync(new object[] { request.Id }, cancellationToken);

        // 2. Validar
        // Nota: Como tu entidad usa "EstadoRegistro", podemos verificar eso también si quieres
        if (entity == null || entity.FechaEliminacion != null)
        {
            throw new KeyNotFoundException($"No se encontró el paciente con ID {request.Id}");
        }

        // 3. SOFT DELETE (LA FORMA CORRECTA)
        // En lugar de asignar propiedad por propiedad, usamos tu método:
        entity.Eliminar("SISTEMA", "Eliminado desde API");

        // 4. Guardar
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
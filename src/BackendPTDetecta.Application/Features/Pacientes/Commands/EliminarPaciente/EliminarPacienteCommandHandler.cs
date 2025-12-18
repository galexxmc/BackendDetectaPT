using MediatR;
using BackendPTDetecta.Application.Common.Interfaces;

namespace BackendPTDetecta.Application.Features.Pacientes.Commands.EliminarPaciente;

public class EliminarPacienteCommandHandler : IRequestHandler<EliminarPacienteCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService; // <--- 1. Agregamos el servicio de usuario

    public EliminarPacienteCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(EliminarPacienteCommand request, CancellationToken cancellationToken)
    {
        // 1. Buscar
        var entity = await _context.Pacientes
            .FindAsync(new object[] { request.Id }, cancellationToken);

        // 2. Validar
        if (entity == null || entity.FechaEliminacion != null)
        {
            throw new KeyNotFoundException($"No se encontró el paciente con ID {request.Id}");
        }

        // 3. OBTENER USUARIO REAL
        var usuario = _currentUserService.CodigoUsuario ?? "ANONIMO";

        // 4. SOFT DELETE
        entity.Eliminar(usuario, request.Motivo);

        // 5. Guardar
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
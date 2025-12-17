using MediatR;

namespace BackendPTDetecta.Application.Features.Pacientes.Commands.EliminarPaciente;

// Usamos "record" para hacerlo en una sola línea. Recibe ID y devuelve nada (Unit).
public record EliminarPacienteCommand(int Id) : IRequest<Unit>;
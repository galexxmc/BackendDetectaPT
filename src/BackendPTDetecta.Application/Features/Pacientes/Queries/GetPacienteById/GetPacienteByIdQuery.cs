using MediatR;

namespace BackendPTDetecta.Application.Features.Pacientes.Queries.GetPacienteById;

public record GetPacienteByIdQuery(int Id) : IRequest<PacienteDetalleDto>;
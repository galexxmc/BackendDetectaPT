using MediatR;

namespace BackendPTDetecta.Application.Features.Pacientes.Queries.GetPacientesList;

public class GetPacientesListQuery : IRequest<List<PacienteDto>>
{
}
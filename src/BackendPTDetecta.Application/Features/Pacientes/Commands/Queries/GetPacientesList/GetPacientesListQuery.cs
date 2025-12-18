using MediatR;
using BackendPTDetecta.Application.Common.Models; // <--- Importante

namespace BackendPTDetecta.Application.Features.Pacientes.Queries.GetPacientesList;

// Cambiamos el retorno de List<PacienteDto> a PaginatedList<PacienteDto>
public class GetPacientesListQuery : IRequest<PaginatedList<PacienteDto>>
{
    public int PageNumber { get; set; } = 1; // Por defecto página 1
    public int PageSize { get; set; } = 10;  // Por defecto 10 registros
    public string? SearchTerm { get; set; } // Nuevo campo para buscar
}
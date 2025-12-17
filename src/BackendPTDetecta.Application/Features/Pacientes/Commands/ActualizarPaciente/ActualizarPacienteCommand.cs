using MediatR;

namespace BackendPTDetecta.Application.Features.Pacientes.Commands.ActualizarPaciente;

// Devolvemos "Unit" (que es como "void" en MediatR) porque no necesitamos retornar un valor, solo confirmar que se hizo.
public class ActualizarPacienteCommand : IRequest<Unit>
{
    public int Id { get; set; } // ¡Importante! Para saber a quién editar
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string Dni { get; set; } = string.Empty;
    public DateOnly FechaNacimiento { get; set; }
    public int SexoId { get; set; }
    public string? Direccion { get; set; }
    public string? Telefono { get; set; }
    public string? Email { get; set; }
}
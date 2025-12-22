using MediatR;

namespace BackendPTDetecta.Application.Features.Pacientes.Commands.CrearPaciente
{
    // IRequest<int> significa: "Este comando devolverá un número entero (el ID del paciente creado)"
    public class CrearPacienteCommand : IRequest<int>
    {
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Dni { get; set; } = string.Empty;
        public DateOnly FechaNacimiento { get; set; }
        public int SexoId { get; set; } // 1: Masc, 2: Fem        
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public int IdTipoSeguro { get; set; }
    }
}
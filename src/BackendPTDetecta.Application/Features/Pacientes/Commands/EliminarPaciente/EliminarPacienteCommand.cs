using MediatR;

namespace BackendPTDetecta.Application.Features.Pacientes.Commands.EliminarPaciente;

// Agregamos la propiedad Motivo
public class EliminarPacienteCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string Motivo { get; set; } = string.Empty; // <--- NUEVO CAMPO

    // Actualizamos el constructor (opcional, pero útil)
    public EliminarPacienteCommand(int id, string motivo)
    {
        Id = id;
        Motivo = motivo;
    }
}
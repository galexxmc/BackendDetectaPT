using MediatR;

namespace BackendPTDetecta.Application.Features.Usuarios.Commands.OlvideContrasena;

public class OlvideContrasenaCommand : IRequest<string>
{
    public string Email { get; set; } = string.Empty;
}
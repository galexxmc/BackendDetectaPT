using MediatR;

namespace BackendPTDetecta.Application.Features.Usuarios.Commands.RegistrarUsuario;

public class RegistrarUsuarioCommand : IRequest<string> // Devuelve string (ID del usuario o mensaje)
{
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
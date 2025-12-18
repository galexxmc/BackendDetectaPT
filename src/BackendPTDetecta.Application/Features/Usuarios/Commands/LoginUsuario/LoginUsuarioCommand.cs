using MediatR;

namespace BackendPTDetecta.Application.Features.Usuarios.Commands.LoginUsuario;

public class LoginUsuarioCommand : IRequest<LoginResponseDto>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
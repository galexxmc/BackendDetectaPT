using MediatR;

namespace BackendPTDetecta.Application.Features.Usuarios.Commands.ResetearContrasena;

public class ResetearContrasenaCommand : IRequest<string>
{
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}
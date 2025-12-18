using MediatR;
using BackendPTDetecta.Application.Common.Interfaces;

namespace BackendPTDetecta.Application.Features.Usuarios.Commands.ResetearContrasena;

public class ResetearContrasenaCommandHandler : IRequestHandler<ResetearContrasenaCommand, string>
{
    private readonly IIdentityService _identityService;

    public ResetearContrasenaCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<string> Handle(ResetearContrasenaCommand request, CancellationToken cancellationToken)
    {
        var (result, errors) = await _identityService.ResetPasswordAsync(request.Email, request.Token, request.NewPassword);

        if (!result)
        {
            throw new Exception($"No se pudo restablecer la contraseña: {string.Join(", ", errors)}");
        }

        return "¡Contraseña actualizada con éxito! Ya puedes iniciar sesión.";
    }
}
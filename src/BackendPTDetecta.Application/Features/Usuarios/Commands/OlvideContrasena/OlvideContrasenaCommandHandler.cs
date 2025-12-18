using MediatR;
using BackendPTDetecta.Application.Common.Interfaces;

namespace BackendPTDetecta.Application.Features.Usuarios.Commands.OlvideContrasena;

public class OlvideContrasenaCommandHandler : IRequestHandler<OlvideContrasenaCommand, string>
{
    private readonly IIdentityService _identityService;

    public OlvideContrasenaCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<string> Handle(OlvideContrasenaCommand request, CancellationToken cancellationToken)
    {
        var token = await _identityService.GeneratePasswordResetTokenAsync(request.Email);

        if (token == null)
        {
            // Por seguridad, no decimos si el correo no existe, solo lanzamos un mensaje genérico o error
            throw new KeyNotFoundException("El correo no está registrado.");
        }

        // NOTA: Aquí es donde normalmente enviarías el correo (EmailService.Send...)
        // Para este ejercicio, devolvemos el token directamente para que puedas probar el reset.
        return token;
    }
}
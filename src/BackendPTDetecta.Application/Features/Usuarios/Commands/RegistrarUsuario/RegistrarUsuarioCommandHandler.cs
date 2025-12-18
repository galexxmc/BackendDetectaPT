using MediatR;
using BackendPTDetecta.Application.Common.Interfaces;
using BackendPTDetecta.Application.Common.Helpers; // Para usar tu Helper

namespace BackendPTDetecta.Application.Features.Usuarios.Commands.RegistrarUsuario;

public class RegistrarUsuarioCommandHandler : IRequestHandler<RegistrarUsuarioCommand, string>
{
    private readonly IIdentityService _identityService;

    public RegistrarUsuarioCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<string> Handle(RegistrarUsuarioCommand request, CancellationToken cancellationToken)
    {
        // 1. Generar el código de usuario (Ej: "gmonje") usando tu Helper
        var codigoUsuario = UserCodeHelper.GenerarCodigo(request.Nombres, request.Apellidos);

        // 2. Llamar al servicio de infraestructura para crear el usuario
        var (result, errors) = await _identityService.CreateUserAsync(
            request.Email, 
            request.Password, 
            request.Nombres, 
            request.Apellidos, 
            codigoUsuario
        );

        if (!result)
        {
            // Si falla, lanzamos una excepción con los errores
            throw new Exception($"Error al registrar: {string.Join(", ", errors)}");
        }

        return $"Usuario registrado con éxito. Código asignado: {codigoUsuario}";
    }
}
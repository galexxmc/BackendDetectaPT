using MediatR;
using BackendPTDetecta.Application.Common.Interfaces;

namespace BackendPTDetecta.Application.Features.Usuarios.Commands.LoginUsuario;

public class LoginUsuarioCommandHandler : IRequestHandler<LoginUsuarioCommand, LoginResponseDto>
{
    private readonly IIdentityService _identityService;

    public LoginUsuarioCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<LoginResponseDto> Handle(LoginUsuarioCommand request, CancellationToken cancellationToken)
    {
        var (result, token, nombre, codigo) = await _identityService.LoginAsync(request.Email, request.Password);

        if (!result)
        {
            throw new UnauthorizedAccessException("Credenciales inválidas.");
        }

        return new LoginResponseDto
        {
            Token = token,
            NombreCompleto = nombre,
            CodigoUsuario = codigo
        };
    }
}
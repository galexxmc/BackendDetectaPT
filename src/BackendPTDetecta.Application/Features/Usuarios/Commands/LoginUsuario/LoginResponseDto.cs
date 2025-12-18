namespace BackendPTDetecta.Application.Features.Usuarios.Commands.LoginUsuario;

public class LoginResponseDto
{
    public string Token { get; set; } = string.Empty;
    public string NombreCompleto { get; set; } = string.Empty;
    public string CodigoUsuario { get; set; } = string.Empty;
}
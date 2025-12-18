namespace BackendPTDetecta.Application.Common.Interfaces;

public interface IIdentityService
{
    // Método para crear usuario. Devuelve (bool Exito, string[] Errores)
    Task<(bool Result, string[] Errors)> CreateUserAsync(string email, string password, string nombres, string apellidos, string codigoUsuario);
    
    // --- NUEVO MÉTODO ---
    // Devuelve: (Éxito, Token, NombreUsuario, CodigoUsuario)
    Task<(bool Result, string Token, string UserName, string CodigoUsuario)> LoginAsync(string email, string password);

    // --- NUEVO: Generar token de recuperación ---
    Task<string?> GeneratePasswordResetTokenAsync(string email);

    // --- NUEVO: Resetear la contraseña ---
    Task<(bool Result, string[] Errors)> ResetPasswordAsync(string email, string token, string newPassword);
}
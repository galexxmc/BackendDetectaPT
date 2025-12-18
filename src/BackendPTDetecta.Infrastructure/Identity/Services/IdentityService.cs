using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using BackendPTDetecta.Application.Common.Interfaces;

namespace BackendPTDetecta.Infrastructure.Identity.Services;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration; // Necesario para leer el appsettings

    public IdentityService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<(bool Result, string[] Errors)> CreateUserAsync(string email, string password, string nombres, string apellidos, string codigoUsuario)
    {
        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            Nombres = nombres,
            Apellidos = apellidos,
            CodigoUsuario = codigoUsuario
        };

        var result = await _userManager.CreateAsync(user, password);

        return (result.Succeeded, result.Errors.Select(e => e.Description).ToArray());
    }

    // --- IMPLEMENTACIÓN DEL LOGIN ---
    public async Task<(bool Result, string Token, string UserName, string CodigoUsuario)> LoginAsync(string email, string password)
    {
        // 1. Buscar usuario por email
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) return (false, string.Empty, string.Empty, string.Empty);

        // 2. Verificar contraseña
        var passwordValido = await _userManager.CheckPasswordAsync(user, password);
        if (!passwordValido) return (false, string.Empty, string.Empty, string.Empty);

        // 3. Generar el Token JWT
        var token = GenerarToken(user);

        return (true, token, $"{user.Nombres} {user.Apellidos}", user.CodigoUsuario);
    }

    private string GenerarToken(ApplicationUser user)
    {
        var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Secret"]!);
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim("CodigoUsuario", user.CodigoUsuario ?? "SIN_CODIGO"), // Guardamos tu código especial dentro del token
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JwtSettings:ExpiryMinutes"]!)),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration["JwtSettings:Issuer"],
            Audience = _configuration["JwtSettings:Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public async Task<string?> GeneratePasswordResetTokenAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) return null;

        // Genera un token largo y seguro diseñado para resetear la contraseña
        return await _userManager.GeneratePasswordResetTokenAsync(user);
    }

    public async Task<(bool Result, string[] Errors)> ResetPasswordAsync(string email, string token, string newPassword)
    {
        var user = await _userManager.FindByEmailAsync(email);
        
        // Por seguridad, si el usuario no existe, decimos que "falló" pero no damos detalles
        if (user == null) return (false, new[] { "Error al restablecer la contraseña." });

        var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

        return (result.Succeeded, result.Errors.Select(e => e.Description).ToArray());
    }
}
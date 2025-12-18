using Microsoft.AspNetCore.Identity;

namespace BackendPTDetecta.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string CodigoUsuario { get; set; } = string.Empty;
    }
}
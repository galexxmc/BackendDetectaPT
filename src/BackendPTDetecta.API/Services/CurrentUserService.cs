using System.Security.Claims;
using BackendPTDetecta.Application.Common.Interfaces;

namespace BackendPTDetecta.API.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? CodigoUsuario
    {
        get
        {
            // Buscamos el claim "CodigoUsuario" que guardamos dentro del Token en el paso anterior
            return _httpContextAccessor.HttpContext?.User?.FindFirst("CodigoUsuario")?.Value;
        }
    }
}
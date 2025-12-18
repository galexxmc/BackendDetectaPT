using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BackendPTDetecta.Infrastructure.Persistence;
using BackendPTDetecta.Infrastructure.Identity;
using BackendPTDetecta.Infrastructure.Identity.Services;
using BackendPTDetecta.Application.Common.Interfaces;

namespace BackendPTDetecta.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // 1. Configurar el sistema de Seguridad (Identity)
        services.AddIdentityCore<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        // 2. Conectar la interfaz IIdentityService con su implementación real
        services.AddTransient<IIdentityService, IdentityService>();

        return services;
    }
}
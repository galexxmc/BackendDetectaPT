using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BackendPTDetecta.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // 1. Configurar MediatR (Busca todos los Commands y Handlers en este proyecto)
            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            // 2. Configurar Validadores (Busca todos los AbstractValidator)
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
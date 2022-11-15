using TelcosAppApi.AplicationServices.Application.Contracts.Roles;
using TelcosAppApi.AplicationServices.Application.Roles;
using TelcosAppApi.DomainServices.Domain.Contracts.Roles;
using TelcosAppApi.DomainServices.Domain.Roles;
using Microsoft.Extensions.DependencyInjection;

namespace TelcosAppApi.DI
{
    /// <summary>
    /// Provee la carga de los perfiles de inyección de dependencias
    /// de toda la solución
    /// </summary>
    public static class DependencyInjectionProfile
    {
        public static void RegisterProfile(IServiceCollection services, IConfiguration configuration)
        {
            
        }

    }
}

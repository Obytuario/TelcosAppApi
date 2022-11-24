using TelcosAppApi.AplicationServices.Application.Contracts.Roles;
using TelcosAppApi.AplicationServices.Application.Roles;
using TelcosAppApi.DomainServices.Domain.Contracts.Roles;
using TelcosAppApi.DomainServices.Domain.Roles;
using Microsoft.Extensions.DependencyInjection;
using TelcosAppApi.AplicationServices.Application.Contracts.Authentication;
using DomainServices.Domain.Contracts.Authentication;
using TelcosAppApi.AplicationServices.Application.Authentication;
using DomainServices.Domain.Authentication;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Runtime;
using TelcosAppApi.DataAccess.Entities;
using TelcosAppApi.DataAccess.DataAccess;

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
            #region Context

            CustomDbSettings val = new CustomDbSettings();

            services.AddDbContext<TelcosSuiteContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("TelcosConnectionString"))
                    .LogTo(System.Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
            });

            #endregion Context

            #region Application
            //services.AddTransient<IMapper, Mapper>();
            services.AddTransient<IRolesServices, RolesAppServices>();
            services.AddTransient<IAuthenticationServices, AuthenticationAppServices>();
           
            #endregion

            #region Domain

            services.AddTransient<IRolesDomain, RolesDomain>();
            services.AddTransient<IAuthenticationDomain, AuthenticationDomain>();
            
            #endregion Domain
        }

    }
}

using AplicationServices.Application.Contracts.Roles;
using AplicationServices.Application.Roles;
using DomainServices.Domain.Contracts.Roles;
using DomainServices.Domain.Roles;
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
using DomainServices.Domain.Contracts.WorkOrderManagement;
using DomainServices.Domain.WorkOrderManagement;
using AplicationServices.Application.Contracts.WorkOrderManagement;
using AplicationServices.Application.WorkOrderManagement;
using AplicationServices.Application.Contracts.User;
using AplicationServices.Application.User;
using DomainServices.Domain.Contracts.User;
using DomainServices.Domain.User;
using AplicationServices.Application.Contracts.Cargos;
using AplicationServices.Application.Cargos;
using DomainServices.Domain.Contracts.cargos;
using DomainServices.Domain.Cargos;
using AplicationServices.Application.Contracts.CentroOperaciones;
using AplicationServices.Application.CentroOperaciones;
using DomainServices.Domain.Contracts.CentroOperaciones;
using DomainServices.Domain.CentroOperaciones;
using AplicationServices.Application.Contracts.Carpetas;
using AplicationServices.Application.Carpetas;
using DomainServices.Domain.Contracts.Carpetas;
using DomainServices.Domain.Carpetas;
using AplicationServices.Application.Contracts.WorkOrderFollowUp;
using AplicationServices.Application.WorkOrderFollowUp;
using DomainServices.Domain.Contracts.WorkOrderFollowUp;
using DomainServices.Domain.WorkOrderFollowUp;
using AplicationServices.Application.Contracts.Subscriber;
using DomainServices.Domain.Contracts.Subscriber;
using DomainServices.Domain.Subscriber;
using AplicationServices.Application.Subscriber;

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
            services.AddTransient<IWorkOrderManagementServices, WorkOrderManagementAppServices>();
            services.AddTransient<IUserServices, UserAppServices>();
            services.AddTransient<ICargosServices, CargosAppServices>();
            services.AddTransient<ICentroOperacionServices, CentroOperacionesAppServices>();
            services.AddTransient<ICarpetasServices, CarpetasAppServices>();
            services.AddTransient<IWorkOrderFollowUpServices, WorkOrderFollowUpServices>();
            services.AddTransient<ISubscriberServices, SubscriberAppServices> ();


            #endregion

            #region Domain

            services.AddTransient<IRolesDomain, RolesDomain>();
            services.AddTransient<IAuthenticationDomain, AuthenticationDomain>();
            services.AddTransient<IWorkOrderManagementDomain, WorkOrderManagementDomain>();
            services.AddTransient<IUserDomain, UserDomain>();
            services.AddTransient<ICargosDomain, CargosDomain>();
            services.AddTransient<ICentroOperacionesDomain, CentroOperacionesDomain>();
            services.AddTransient<ICarpetaDomain, CarpetasDomain>();
            services.AddTransient<IWorkOrderFollowUpDomain, WorkOrderFollowUpDomain>();
            services.AddTransient<ISubscriberDomain, SubscriberDomain>();

            #endregion Domain
        }

    }
}

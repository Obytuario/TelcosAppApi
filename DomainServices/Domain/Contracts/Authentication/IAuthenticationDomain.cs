using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelcosAppApi.DataAccess.Entities;

namespace DomainServices.Domain.Contracts.Authentication
{
    public interface IAuthenticationDomain
    {
        Task<Usuario?> Login(Usuario credencialesUsuario);
        bool? CargaMasiva(List<ParamEquipoActividad> paramEquipoActividads, List<ParamMaterialActividad> paramMaterialActividads);

        Task<ParamEquipoActividad?> ConsultarParamEquipo(Guid actividad, Guid equipo);

        Task<ParamMaterialActividad?> ConsultarParamMaterial(Guid actividad, Guid material);
       
    }
}

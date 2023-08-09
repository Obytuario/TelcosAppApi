using DomainServices.Domain.Contracts.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TelcosAppApi.DataAccess.DataAccess;
using TelcosAppApi.DataAccess.Entities;

namespace DomainServices.Domain.Authentication
{
    public class AuthenticationDomain : IAuthenticationDomain
    {
        private readonly TelcosSuiteContext _context;
        public AuthenticationDomain(TelcosSuiteContext telcosApiContext)
        {
            _context = telcosApiContext;
        }

        public async Task<Usuario?> Login(Usuario credencialesUsuario)
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(i => i.NumeroDocumento.Equals(credencialesUsuario.NumeroDocumento) && i.Contraseña.Equals(credencialesUsuario.Contraseña));

            if (usuario == null)
                return null;

            return usuario;

        }

        public bool? CargaMasiva(List<ParamEquipoActividad> paramEquipoActividads,List<ParamMaterialActividad> paramMaterialActividads)
        {
            _context.ParamEquipoActividad.AddRange(paramEquipoActividads);
            _context.ParamMaterialActividad.AddRange(paramMaterialActividads);
            _context.SaveChanges();
            return true;

        }
        public async Task<ParamEquipoActividad?> ConsultarParamEquipo(Guid actividad, Guid equipo)
        {
            var param = await _context.ParamEquipoActividad.FirstOrDefaultAsync(i => i.Actividad.Equals(actividad) && i.Equipo.Equals(equipo));

            if (param == null)
                return null;

            return param;

        }
        public async Task<ParamMaterialActividad?> ConsultarParamMaterial(Guid actividad, Guid material)
        {
            var param = await _context.ParamMaterialActividad.FirstOrDefaultAsync(i => i.Actividad.Equals(actividad) && i.Material.Equals(material));

            if (param == null)
                return null;

            return param;

        }
    }
}

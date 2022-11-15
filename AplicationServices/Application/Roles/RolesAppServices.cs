using TelcosAppApi.AplicationServices.Application.Contracts.Roles;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelcosAppApi.DataAccess.Entities;
using TelcosAppApi.DomainServices.Domain.Contracts.Roles;

namespace TelcosAppApi.AplicationServices.Application.Roles
{
    public class RolesAppServices :IRolesServices
    {
        /// <summary>
        /// Instancia al servicio de Dominio
        /// </summary>
        private readonly IRolesDomain _rolesDomain;
        public RolesAppServices(IRolesDomain rolesDomain)
        {
            _rolesDomain = rolesDomain;
        }
        #region Method
        public async Task<List<Rol>> GetRoles()
        {
            try
            {
                //var claims = HttpContext.User.Claims.Where(claim => claim.Type == "id").FirstOrDefault();
                return await _rolesDomain.GetRoles();
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}

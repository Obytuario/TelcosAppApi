using AplicationServices.DTOs.Generics;
using AplicationServices.DTOs.workOrderManagement;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using AplicationServices.Application.Contracts.Roles;
using TelcosAppApi.DataAccess;
using TelcosAppApi.DataAccess.DataAccess;
using TelcosAppApi.DataAccess.Entities;


namespace TelcosAppApi.Controllers
{
    [ApiController]
    [Route("Api/Roles")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RolesController:ControllerBase
    {
        #region Fiedls

        /// <summary>
        /// Instancia al servicio de aplicación
        /// </summary>
        private readonly IRolesServices _rolesServices;
      

        #endregion Fiedls
        public RolesController(IRolesServices rolesServices)
        {
            _rolesServices = rolesServices;
        }

        [HttpGet]
        public async Task <RequestResult<List<GenericDto>>> Get() 
        {            
            return await _rolesServices.GetRoles();
        }
    }
}

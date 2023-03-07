using AplicationServices.Application.Contracts.CentroOperaciones;
using AplicationServices.Application.Contracts.Roles;
using AplicationServices.DTOs.Generics;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TelcosAppApi.Controllers
{
    [ApiController]
    [Route("Api/CentroOperaciones")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CentroOperacionesController : Controller
    {
        #region Fiedls

        /// <summary>
        /// Instancia al servicio de aplicación
        /// </summary>
        private readonly ICentroOperacionServices _centroOperacionesServices;


        #endregion Fiedls
        public CentroOperacionesController(ICentroOperacionServices centroOperacionesServices)
        {
            _centroOperacionesServices = centroOperacionesServices;
        }

        /// <summary>
        /// Obtiene todos los roles del sistema
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        /// <author>Ariel Bejarano</author>
        [HttpGet("GetAllOperationCenter")]
        public async Task<RequestResult<List<GenericDto>>> Get()
        {
            return await _centroOperacionesServices.GetCentroOperaciones();
        }
    }
}

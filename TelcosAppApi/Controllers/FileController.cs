using AplicationServices.Application.Contracts.Carpetas;
using AplicationServices.Application.Contracts.Roles;
using AplicationServices.DTOs.Generics;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TelcosAppApi.Controllers
{
    [ApiController]
    [Route("Api/Files")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FileController : ControllerBase
    {
        #region Fiedls

        /// <summary>
        /// Instancia al servicio de aplicación
        /// </summary>
        private readonly ICarpetasServices _carpetasServices;


        #endregion Fiedls
        public FileController(ICarpetasServices carpetasServices)
        {
            _carpetasServices = carpetasServices;
        }

        /// <summary>
        /// Obtiene todos los roles del sistema
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        /// <author>Ariel Bejarano</author>
        [HttpGet("GetAllFile")]
        public async Task<RequestResult<List<GenericDto>>> Get()
        {
            return await _carpetasServices.GetFiles();
        }
    }
}

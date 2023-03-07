using AplicationServices.Application.Contracts.Cargos;
using AplicationServices.Application.Contracts.Roles;
using AplicationServices.DTOs.Generics;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TelcosAppApi.Controllers
{
    [ApiController]
    [Route("Api/Cargos")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CargoController : ControllerBase
    {
        /// <summary>
        /// Instancia al servicio de aplicación
        /// </summary>
        private readonly ICargosServices _cargosServices;



        public CargoController(ICargosServices cargosServices)
        {
            _cargosServices = cargosServices;
        }

        /// <summary>
        /// Obtiene todos los roles del sistema
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        /// <author>Ariel Bejarano</author>
        [HttpGet("GetAllCargo")]
        public async Task<RequestResult<List<GenericDto>>> Get()
        {
            return await _cargosServices.GetCargos();
        }
    }
}

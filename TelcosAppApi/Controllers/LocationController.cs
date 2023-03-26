using AplicationServices.Application.Contracts.Location;
using AplicationServices.Application.Contracts.User;
using AplicationServices.DTOs.Generics;
using AplicationServices.DTOs.Location;
using AplicationServices.DTOs.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TelcosAppApi.Controllers
{
    [ApiController]
    [Route("Api/Location")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LocationController : ControllerBase
    {
        #region Fiedls

        /// <summary>
        /// Instancia al servicio de aplicación
        /// </summary>
        private readonly ILocationServices _locationServices;


        #endregion Fiedls
        public LocationController(ILocationServices locationServices)
        {
            _locationServices = locationServices;
        }
        /// <summary>
        /// Guarda la informacion de un usuario
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        /// <author>Ariel Bejarano</author>
        [HttpPost("SaveLocationUser")]
        public async Task<RequestResult<string>> SaveLocationUser(LocationDto userDto)
        {
            return await _locationServices.SaveLocationUser(userDto);
        }
        /// <summary>
        /// obtener l aubicacion de todos los usuarios del dia
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        /// <author>Ariel Bejarano</author>
        [HttpGet("GetAllLocationUser")]
        public async Task<RequestResult<List<LocationDto>>> GetAllLocationUser(Guid user)
        {
            return await _locationServices.GetAllLocationUser(user);
        }
    }
}

using AplicationServices.Application.Contracts.User;
using AplicationServices.DTOs.Generics;
using AplicationServices.DTOs.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TelcosAppApi.Controllers
{
    [ApiController]
    [Route("Api/User")]  
    public class UserController : ControllerBase
    {
        #region Fiedls

        /// <summary>
        /// Instancia al servicio de aplicación
        /// </summary>
        private readonly IUserServices _userServices;


        #endregion Fiedls
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        /// <summary>
        /// Guarda la informacion de un usuario
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        /// <author>Ariel Bejarano</author>
        [HttpPost("SaveUser")]
        public async Task<RequestResult<PostUserDto>> SaveWorkOrder(PostUserDto userDto)
        {
            return await _userServices.SaveUser(userDto);
        }
    }
}

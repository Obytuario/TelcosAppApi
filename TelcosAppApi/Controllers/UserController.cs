using AplicationServices.Application.Contracts.User;
using AplicationServices.DTOs.Generics;
using AplicationServices.DTOs.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TelcosAppApi.Controllers
{
    [ApiController]
    [Route("Api/User")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        /// Obtiene todos los usuarios del sistema
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        /// <author>Ariel Bejarano</author>
        [HttpGet("GetAllUsers")]
        public async Task<RequestResult<List<PostUserDto>>> GetAllUsers()
        {
            return await _userServices.GetAllUsers();
        }
        /// <summary>
        /// Obtiene todos los usuarios asignados
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <author>Ariel Bejarano</author>
        [HttpGet("GetAssignmentById")]
        public async Task<RequestResult<List<PostUserDto>>> GetUserAssignmentById(Guid user)
        {
            return await _userServices.GetUserAssignmentById(user);
        }
        /// <summary>
        /// Obtiene todos los usuarios que se pueden asignar por rol
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <author>Ariel Bejarano</author>
        [HttpGet("GetAssignmentByRol")]
        public async Task<RequestResult<List<PostUserDto>>> GetAssignmentByRol(Guid rol)
        {
            return await _userServices.GetAssignmentByRol(rol);
        }


        /// <summary>
        /// Guarda la informacion de un usuario
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        /// <author>Ariel Bejarano</author>
        [HttpPost("SaveUser")]
        public async Task<RequestResult<PostUserDto>> SaveUser(PostUserDto userDto)
        {
            return await _userServices.SaveUser(userDto);
        }
        /// <summary>
        /// Guarda la informacion de un usuario
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        /// <author>Ariel Bejarano</author>
        [HttpPost("SaveAssignmentUser")] 
        public async Task<RequestResult<PostUserDto>> SaveAssignmentUser(PostUserDto userDto)
        {
            return await _userServices.UpdateUser(userDto);
        }
        /// <summary>
        /// Guarda la informacion de un usuario
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        /// <author>Ariel Bejarano</author>
        [HttpPost("UpdateUser")]
        public async Task<RequestResult<PostUserDto>> UpdateUser(PostUserDto userDto)
        {
            return await _userServices.UpdateUser(userDto);
        }

    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TelcosAppApi.DataAccess;
using TelcosAppApi.DataAccess.DataAccess;
using TelcosAppApi.AplicationServices.DTOs.Authentication;
using TelcosAppApi.AplicationServices.Application.Contracts.Roles;
using AplicationServices.Application.Contracts.Authentication;

namespace TelcosAppApi.Controllers
{
    [ApiController]
    [Route("Api/Authentication")]
    public class AuthenticationController:ControllerBase
    {

        /// <summary>
        /// Instancia al servicio de aplicación
        /// </summary>
        private readonly IAuthenticationServices _AuthenticationServices;
      
        public AuthenticationController(IAuthenticationServices authenticationServices)        {      
           
            _AuthenticationServices = authenticationServices;   
        }

        [HttpPost("login", Name = "loginUsuario")]
        public async Task<ActionResult<RespuestaAutenticacionDto>> Login(CredencialesUsuarioDto credencialesUsuario)
        {
            var resultado = await _AuthenticationServices.Login(credencialesUsuario);

            if (resultado != null)
            {
                return resultado;
            }
            else
            {
                return BadRequest("Login incorrecto");
            }
        }
    }
}

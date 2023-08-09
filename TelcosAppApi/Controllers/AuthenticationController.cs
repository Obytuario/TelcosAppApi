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
using AplicationServices.Application.Contracts.Roles;
using TelcosAppApi.AplicationServices.Application.Contracts.Authentication;
using TelcosAppApi.DataAccess.Entities;
using AutoMapper;
using AplicationServices.DTOs.Generics;

namespace TelcosAppApi.Controllers
{
    [ApiController]
    [Route("Api/Authentication")]
    //[Produces("application/json")]
    public class AuthenticationController:ControllerBase
    {

        /// <summary>
        /// Instancia al servicio de aplicación
        /// </summary>
        private readonly IAuthenticationServices _AuthenticationServices;
        private readonly IMapper _mapper;

        public AuthenticationController(IAuthenticationServices authenticationServices,IMapper mapper)        {      
           
            _AuthenticationServices = authenticationServices;
            _mapper = mapper;
        }

        [HttpPost("login", Name = "loginUsuario")]
        public async Task<RequestResult<RespuestaAutenticacionDto>> Login(CredencialesUsuarioDto credencialesUsuario)
        {            
            return await _AuthenticationServices.Login(credencialesUsuario);           
        }
        [HttpPost("CargaMasiva")]
        public async Task<RequestResult<string>> CargaMasiva(CargaDto cargaDto)
        {
            return await _AuthenticationServices.CargaMasiva(cargaDto);
        }
    }
}

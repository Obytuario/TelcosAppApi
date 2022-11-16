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

namespace TelcosAppApi.Controllers
{
    [ApiController]
    [Route("Api/Authentication")]
    public class AuthenticationController:ControllerBase
    {
        private readonly TelcosApiContext Context;
        private readonly IConfiguration configuration;
        public AuthenticationController(TelcosApiContext context, IConfiguration configuration)
        {
            Context = context;
            this.configuration = configuration;
        }

        [HttpPost("login", Name = "loginUsuario")]
        public async Task<ActionResult<RespuestaAutenticacionDto>> Login(CredencialesUsuarioDto credencialesUsuario)
        {
            var resultado = await Context.Usuario.FirstOrDefaultAsync(i => i.NumeroDocumento.Equals(credencialesUsuario.User)&&i.Contraseña.Equals(credencialesUsuario.Password));

            if (resultado != null)
            {
                return ConstruirToken(credencialesUsuario, resultado.ID);
            }
            else
            {
                return BadRequest("Login incorrecto");
            }
        }

        private RespuestaAutenticacionDto ConstruirToken(CredencialesUsuarioDto credencialesUsuario, Guid idUser)
        {
            var claims = new List<Claim>()
            {
                new Claim("user", credencialesUsuario.User??""),
                new Claim("id", idUser.ToString())
            };

            //var usuario = await userManager.FindByEmailAsync(credencialesUsuario.Email);
            //var claimsDB = await userManager.GetClaimsAsync(usuario);

            //claims.AddRange(claimsDB);

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["KeyJwt"]));
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddYears(1);

            var securityToken =  new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expiracion, signingCredentials: creds);

            return new RespuestaAutenticacionDto()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                //Expiracion = expiracion
            };
        }

    }
}

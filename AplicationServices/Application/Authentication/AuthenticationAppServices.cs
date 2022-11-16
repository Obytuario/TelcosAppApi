using AplicationServices.Application.Contracts.Authentication;
using AutoMapper;
using DomainServices.Domain.Contracts.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TelcosAppApi.AplicationServices.DTOs.Authentication;
using TelcosAppApi.DataAccess.Entities;
using TelcosAppApi.DomainServices.Domain.Contracts.Roles;

namespace AplicationServices.Application.Authentication
{
    public class AuthenticationAppServices : IAuthenticationServices
    {
        /// <summary>
        /// Instancia al servicio de Dominio
        /// </summary>
        private readonly IAuthenticationDomain _authenticationDomain;
        /// <summary>
        /// Mapper
        /// </summary>
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public AuthenticationAppServices(IAuthenticationDomain authenticationDomain, IConfiguration configuration, Mapper mapper)
        {
            _authenticationDomain = authenticationDomain;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<RespuestaAutenticacionDto?> Login(CredencialesUsuarioDto credencialesUsuario)
        {
            try
            {

                Usuario usuario = _mapper.Map<CredencialesUsuarioDto,Usuario>(credencialesUsuario);

             
                var result = await _authenticationDomain.Login(usuario);
                if (result == null)
                    return null;

                return ConstruirToken(credencialesUsuario, result.ID);
            }
            catch (Exception)
            {
                throw;
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

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["KeyJwt"]));
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddYears(1);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expiracion, signingCredentials: creds);

            return new RespuestaAutenticacionDto()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                //Expiracion = expiracion
            };
        }
    }
}

using TelcosAppApi.AplicationServices.Application.Contracts.Authentication;
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
using AplicationServices.Helpers.HashResource;
using AplicationServices.DTOs.Generics;
using AplicationServices.DTOs.User;
using AplicationServices.Helpers.TextResorce;
using DomainServices.Domain.Contracts.User;
using Microsoft.VisualBasic;
using AplicationServices.DTOs.Location;
using Microsoft.Extensions.DependencyInjection;
using AplicationServices.ScopeService;
using AplicationServices.Application.Contracts.Location;

namespace TelcosAppApi.AplicationServices.Application.Authentication
{
    public class AuthenticationAppServices : IAuthenticationServices
    {
        /// <summary>
        /// Instancia al servicio de Dominio
        /// </summary>
        private readonly IAuthenticationDomain _authenticationDomain;
        private readonly IUserDomain _userDomain;
        /// <summary>
        /// Mapper
        /// </summary>
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly Hash _hash;
        private readonly ILocationServices _LocationServices; // => _serviceScope.GetService<ILocationServices>();
      
        public AuthenticationAppServices(IAuthenticationDomain authenticationDomain, IUserDomain userDomain, IConfiguration configuration, IMapper mapper, ILocationServices locationServices)
        {
            _authenticationDomain = authenticationDomain;
            _userDomain = userDomain;
            _configuration = configuration;
            _mapper = mapper;
            _hash = new Hash();          
            _LocationServices = locationServices;
        }

        public async Task<RequestResult<RespuestaAutenticacionDto>> Login(CredencialesUsuarioDto credencialesUsuario)
        {
            try
            {
                #region Validaciones
                List<string> errorMessageValidations = new List<string>();
                LoginValidations(ref errorMessageValidations, credencialesUsuario);
                if (errorMessageValidations.Any())
                    return RequestResult<RespuestaAutenticacionDto>.CreateUnsuccessful(errorMessageValidations);
                var user = await _userDomain.GetUser(credencialesUsuario.User);
                if (user == null)
                {
                    errorMessageValidations.Add(ResourceUserMsm.CredentialsInvalidate);
                    return RequestResult<RespuestaAutenticacionDto>.CreateUnsuccessful(errorMessageValidations);
                }
                #endregion

                /*comparacion de hash*/               
                bool isHash = _hash.GetHash(credencialesUsuario.Password, Convert.FromBase64String(user.Salt)).Hash.Equals(user.Contraseña);
                if (!isHash)
                    return RequestResult<RespuestaAutenticacionDto>.CreateUnsuccessful(new string[] { ResourceUserMsm.CredentialsInvalidate });
                /*Guardado Ubicacion*/
                GenerateLocation(user, credencialesUsuario);
                /*Construccion de token*/
                return RequestResult<RespuestaAutenticacionDto>.CreateSuccessful(ConstruirToken(credencialesUsuario, user.ID)); 
              
            }
            catch (Exception ex)
            {
                return RequestResult<RespuestaAutenticacionDto>.CreateError(ex.Message);
               
            }
        }

        #region Private Methods
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
                UserID = idUser
                //Expiracion = expiracion
            };
        }
        /// <summary>
        ///     valida los datos para crear un usuario.
        /// </summary>
        /// <author>Ariel Bejarano</author>
        /// <param name="userDto">objeto para guardar orden de trabajo</param>
        private void LoginValidations(ref List<string> errorMessageValidations, CredencialesUsuarioDto credencialesUsuarioDto)
        {
            if (string.IsNullOrEmpty(credencialesUsuarioDto.User))
            {
                errorMessageValidations.Add(ResourceUserMsm.InvalidParameterDocument);
            }
            if (string.IsNullOrEmpty(credencialesUsuarioDto.Password))
            {
                errorMessageValidations.Add(ResourceUserMsm.InvalidParameterDocument);
            }            
        }
        /// <summary>
        ///     valida los datos para crear un usuario.
        /// </summary>
        /// <author>Ariel Bejarano</author>
        /// <param name="userDto">objeto para guardar orden de trabajo</param>
        private async void GenerateLocation(Usuario user, CredencialesUsuarioDto credencialesUsuarioDto)
        {
            var userLocation = _mapper.Map<CredencialesUsuarioDto, LocationDto> (credencialesUsuarioDto);
            userLocation.IdUser = user.ID;
            await _LocationServices.SaveLocationUser(userLocation);
        }
        #endregion
    }
}

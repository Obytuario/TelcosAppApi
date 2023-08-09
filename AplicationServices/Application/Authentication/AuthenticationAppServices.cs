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
                return RequestResult<RespuestaAutenticacionDto>.CreateSuccessful(ConstruirToken(credencialesUsuario, user)); 
              
            }
            catch (Exception ex)
            {
                return RequestResult<RespuestaAutenticacionDto>.CreateError(ex.Message);
               
            }
        }
        public async Task<RequestResult<string>> CargaMasiva(CargaDto cargaDto)
        {
            try
            {
               List<ParamEquipoActividad> equipoActividad = new List<ParamEquipoActividad>();
               List<ParamMaterialActividad> materialActividad = new List<ParamMaterialActividad>();

                cargaDto.Actividades.ForEach(actividad => {
                    if(equipoActividad.Count>0 || materialActividad.Count>0)
                    {
                        var resultado =  _authenticationDomain.CargaMasiva(equipoActividad, materialActividad);
                        if(resultado??false)
                        {
                            equipoActividad = new List<ParamEquipoActividad>();
                            materialActividad = new List<ParamMaterialActividad>();
                        }
                    }

                    cargaDto.Equipos.ForEach(equipo => {
                        ParamEquipoActividad wresult =  _authenticationDomain.ConsultarParamEquipo(Guid.Parse(actividad.Actividad), Guid.Parse(equipo.Equipo)).Result;
                        if(wresult == null)
                        {
                            ParamEquipoActividad paramEquipoActividad = new ParamEquipoActividad();
                            paramEquipoActividad.ID = Guid.NewGuid();
                            paramEquipoActividad.Equipo = Guid.Parse(equipo.Equipo);
                            paramEquipoActividad.Actividad = Guid.Parse(actividad.Actividad);
                            paramEquipoActividad.Activo = true;
                            equipoActividad.Add(paramEquipoActividad);
                        }
                    });
                    cargaDto.Materiales.ForEach(material => {
                        ParamMaterialActividad wresult = _authenticationDomain.ConsultarParamMaterial(Guid.Parse(actividad.Actividad), Guid.Parse(material.Material)).Result;
                        if (wresult == null)
                        {
                            ParamMaterialActividad paramMaterialActividad = new ParamMaterialActividad();
                            paramMaterialActividad.ID = Guid.NewGuid();
                            paramMaterialActividad.Material = Guid.Parse(material.Material);
                            paramMaterialActividad.Actividad = Guid.Parse(actividad.Actividad);
                            paramMaterialActividad.Activo = true;
                            materialActividad.Add(paramMaterialActividad);
                        }
                    });

                });

                

               return RequestResult<string>.CreateSuccessful("Proceso Exitoso");
            }
            catch (Exception ex)
            {
                return RequestResult<string>.CreateError(ex.Message);

            }
        }


        #region Private Methods
        private RespuestaAutenticacionDto ConstruirToken(CredencialesUsuarioDto credencialesUsuario, Usuario User)
        {
            var claims = new List<Claim>()
            {
                new Claim("user", credencialesUsuario.User??""),
                new Claim("id", User.ID.ToString())
            };

            //var usuario = await userManager.FindByEmailAsync(credencialesUsuario.Email);
            //var claimsDB = await userManager.GetClaimsAsync(usuario);

            //claims.AddRange(claimsDB);

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["KeyJwt"]));
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddDays(2);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expiracion, signingCredentials: creds);

            return new RespuestaAutenticacionDto()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                UserID = User.ID,
                NameUser = string.Concat(User.PrimerNombre ?? "", " ", User.Apellidos ?? ""),
                ChargeUser = User.CargoNavigation?.Descripcion??"",
                RolCode = User.RolNavigation?.Codigo??""
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

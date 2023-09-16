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
using DomainServices.Domain.Contracts.cargos;
using DomainServices.Domain.Contracts.CentroOperaciones;
using DomainServices.Domain.Contracts.Roles;
using Microsoft.EntityFrameworkCore.Metadata;
using AplicationServices.Application.Contracts.User;

namespace TelcosAppApi.AplicationServices.Application.Authentication
{
    public class AuthenticationAppServices : IAuthenticationServices
    {
        /// <summary>
        /// Instancia al servicio de Dominio
        /// </summary>
        private readonly IAuthenticationDomain _authenticationDomain;
        private readonly IUserDomain _userDomain;
        private readonly ICargosDomain _cargosDomain;
        private readonly ICentroOperacionesDomain _centroOperacionDomain;
        private readonly IRolesDomain _rolesDomain;       
        /// <summary>
        /// Mapper
        /// </summary>
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly Hash _hash;
        private readonly ILocationServices _LocationServices; // => _serviceScope.GetService<ILocationServices>();
        private readonly IUserServices _userServices;      
        public AuthenticationAppServices(IAuthenticationDomain authenticationDomain,ICentroOperacionesDomain centroOperacionesDomain, ICargosDomain cargosDomain,  IUserDomain userDomain, IRolesDomain rolesDomain, IConfiguration configuration, IMapper mapper, ILocationServices locationServices, IUserServices userServices)
        {
            _authenticationDomain = authenticationDomain;
            _userDomain = userDomain;
            _rolesDomain = rolesDomain;
            _centroOperacionDomain = centroOperacionesDomain;
            _cargosDomain = cargosDomain;
            _configuration = configuration;
            _mapper = mapper;
            _hash = new Hash();          
            _LocationServices = locationServices;
            _userServices = userServices;
        }

        public async Task<RequestResult<RespuestaAutenticacionDto>> Login(CredencialesUsuarioDto credencialesUsuario)
        {
            try
            {
                #region Validaciones
                List<string> errorMessageValidations = new List<string>();
                LoginValidations(ref errorMessageValidations, credencialesUsuario);
                if (errorMessageValidations.Any())
                    return RequestResult<RespuestaAutenticacionDto>.CreateUnsuccessful(null,errorMessageValidations);
                var user = await _userDomain.GetUser(credencialesUsuario.User);
                if (user == null)
                {
                    errorMessageValidations.Add(ResourceUserMsm.CredentialsInvalidate);
                    return RequestResult<RespuestaAutenticacionDto>.CreateUnsuccessful(null, errorMessageValidations);
                }
                #endregion
                /*Construccion de token*/
                RespuestaAutenticacionDto respuestaAutenticacionDto = ConstruirToken(credencialesUsuario, user);
                /*comparacion de hash*/
                bool isHash = _hash.GetHash(credencialesUsuario.Password, Convert.FromBase64String(user.Salt)).Hash.Equals(user.Contraseña);
                if (!isHash)
                    return RequestResult<RespuestaAutenticacionDto>.CreateUnsuccessful(respuestaAutenticacionDto, new string[] { ResourceUserMsm.CredentialsInvalidate });
                /*Guardado Ubicacion*/
                GenerateLocation(user, credencialesUsuario);
                

                /*reconstruccion de contraseña*/
                if(respuestaAutenticacionDto.IsExpiration)
                    return RequestResult<RespuestaAutenticacionDto>.CreateUnsuccessful(respuestaAutenticacionDto,new string[] { ResourceUserMsm.CredentialsInvalidate });

                return RequestResult<RespuestaAutenticacionDto>.CreateSuccessful(respuestaAutenticacionDto); 
              
            }
            catch (Exception ex)
            {
                return RequestResult<RespuestaAutenticacionDto>.CreateError(ex.Message);
               
            }
        }
        public async Task<RequestResult<RespuestaAutenticacionDto>> RestoreLogin(CredencialesUsuarioDto credencialesUsuario)
        {
            try
            {
                #region Validaciones
                List<string> errorMessageValidations = new List<string>();
                Usuario user = new Usuario();
                LoginValidations(ref errorMessageValidations, credencialesUsuario);
                if (errorMessageValidations.Any())
                    return RequestResult<RespuestaAutenticacionDto>.CreateUnsuccessful(null, errorMessageValidations);
                Usuario userFind = await _userDomain.GetUserById(Guid.Parse(credencialesUsuario.User));
                if (userFind == null)
                {
                    errorMessageValidations.Add(ResourceUserMsm.CredentialsInvalidate);
                    return RequestResult<RespuestaAutenticacionDto>.CreateUnsuccessful(null, errorMessageValidations);
                }
                #endregion     

              
                
                /*Construccion de token*/
                RespuestaAutenticacionDto respuestaAutenticacionDto = ConstruirToken(credencialesUsuario, userFind);

                /*reconstruccion de contraseña*/
                if (!respuestaAutenticacionDto.IsExpiration)
                    return RequestResult<RespuestaAutenticacionDto>.CreateUnsuccessful(respuestaAutenticacionDto, new string[] { ResourceUserMsm.CredentialsInvalidate });

                //detereminar contraseña generica en el config o una tabla de variables genericas
                user = userFind;
                user.Contraseña = credencialesUsuario.Password;
                var hash = _hash.GetHash(user.Contraseña);
                user.Contraseña = hash.Hash;
                user.GenerarContraseña = false;
                user.Salt = Convert.ToBase64String(hash.SaltHash);

                /*Guardado Ubicacion*/
                GenerateLocation(user, credencialesUsuario);

                _userDomain.UpdateUser(user, userFind);
                return RequestResult<RespuestaAutenticacionDto>.CreateSuccessful(respuestaAutenticacionDto);

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
        public async Task<RequestResult<List<CargaUserDto>>> CargaMasivaUsuarios(List<CargaUserDto> userDtos)
        {
            try
            {
                var cargos = _cargosDomain.GetCargos().Result;
                var centros = _centroOperacionDomain.GetCentroOperaciones().Result;
                var roles = _rolesDomain.GetRoles().Result;
                List<CargaUserDto> cargaUserDtosRes = new List<CargaUserDto>();
                foreach (var cargaUserDto in userDtos)
                {
                    var existeUsuario = _userDomain.GetUser(cargaUserDto.Cedula.ToString()).Result;
                    if (existeUsuario == null)
                    {
                        var cargoId = cargos.Where(x => x.Descripcion.Trim().ToUpper().Contains(cargaUserDto.Cargo.Trim().ToUpper())).FirstOrDefault();
                        if (cargoId == null)
                        {
                            cargaUserDto.Observacion = "No existe Cargo";
                            cargaUserDtosRes.Add(cargaUserDto);
                            continue;
                        }
                        var rolId = roles.Where(x => x.Descripcion.Trim().ToUpper().Contains(cargaUserDto.Rol.Trim().ToUpper())).FirstOrDefault();
                        if (rolId == null)
                        {
                            cargaUserDto.Observacion = "No existe Rol";
                            cargaUserDtosRes.Add(cargaUserDto);
                            continue;
                        }
                        var centroId = centros.Where(x => x.Descripcion.Trim().ToUpper().Contains(cargaUserDto.CentroDeOperaciones.Trim().ToUpper())).FirstOrDefault();
                        if (centroId == null)
                        {
                            cargaUserDto.Observacion = "No existe Centro de operaciones";
                            cargaUserDtosRes.Add(cargaUserDto);
                            continue;
                        }

                        // dividir nombre
                        var ArrayNombre = cargaUserDto.Nombre.Trim().Split(' ');
                        string Nombres = ConcatenateFromArray(ArrayNombre, 2, ArrayNombre.Length);
                        string Apellidos = ConcatenateFromArray(ArrayNombre, 0, 2);

                        PostUserDto postUserDto = new PostUserDto();
                        postUserDto.id = Guid.NewGuid();
                        postUserDto.idRol = rolId.ID;
                        postUserDto.idOperationCenter = centroId.ID;
                        postUserDto.idCharge = cargoId.ID;
                        postUserDto.numberDocument = cargaUserDto.Cedula.ToString();
                        postUserDto.fName = Nombres;
                        postUserDto.lName = Apellidos;
                        postUserDto.active = false;
                        postUserDto.email = cargaUserDto.Correo.Trim();
                        postUserDto.mobile = cargaUserDto.Telefono.ToString();

                        var resultUser = _userServices.SaveUser(postUserDto).Result;
                        if (resultUser.IsSuccessful)
                        {
                            cargaUserDto.Observacion = "Usuario Creado";
                            cargaUserDtosRes.Add(cargaUserDto);
                        }
                        
                    }
                    else
                    {
                        cargaUserDto.Observacion = "Usuario ya existe con ese numero de documento.";
                        cargaUserDtosRes.Add(cargaUserDto);
                    }
                }

                return RequestResult<List<CargaUserDto>>.CreateSuccessful(cargaUserDtosRes);
                static string ConcatenateFromArray(string[] array, int startIndex, int endIndex)
                {
                    if (startIndex < 0 || startIndex >= array.Length)
                    {
                        throw new ArgumentException("Índice de inicio fuera de los límites del array.");
                    }

                    string result = "";
                    for (int i = startIndex; i < endIndex; i++)
                    {
                        result += array[i]+" ";
                    }
                    return result.Trim();
                }
            }
            catch (Exception ex)
            {
                return RequestResult<List<CargaUserDto>>.CreateError(ex.Message);

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
                RolCode = User.RolNavigation?.Codigo??"",
                IsExpiration = User.GenerarContraseña
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
                errorMessageValidations.Add(ResourceUserMsm.NotExistUser);
            }
            if (string.IsNullOrEmpty(credencialesUsuarioDto.Password))
            {
                errorMessageValidations.Add(ResourceUserMsm.PasswordNotExist);
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

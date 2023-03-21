using AplicationServices.Application.Contracts.User;
using AplicationServices.DTOs.Generics;
using AplicationServices.DTOs.User;
using AplicationServices.DTOs.workOrderManagement;
using AplicationServices.Helpers.HashResource;
using AutoMapper;
using DomainServices.Domain.Contracts.User;
using DomainServices.Domain.Contracts.WorkOrderManagement;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelcosAppApi.DataAccess.Entities;
using AplicationServices.Helpers.TextResorce;
using TelcosAppApi.AplicationServices.DTOs.Authentication;

namespace AplicationServices.Application.User
{
    public class UserAppServices : IUserServices
    {
        private readonly IMapper _mapper;
        private readonly IUserDomain _userDomain;
        private readonly Hash _hash;
        public UserAppServices(IUserDomain userDomain, IMapper mapper)
        {
            _userDomain = userDomain;
            _mapper = mapper;
            _hash = new Hash();
        }
        /// <summary>
        ///     Obtiene la lista de usuarios del sitema.
        /// </summary>
        /// <author>Ariel Bejarano</author>    
        public async Task<RequestResult<List<PostUserDto>>> GetAllUsers()
        {
            try
            {
                List<string> errorMessageValidations = new List<string>();
                //var user = _mapper.Map<PostUserDto, Usuario>(userDto);

                //if (errorMessageValidations.Any())
                //    return RequestResult<PostUserDto>.CreateUnsuccessful(errorMessageValidations);
                var findUser = await _userDomain.GetAllUser();
                var Users = _mapper.Map<List<Usuario>,List<PostUserDto>>(await _userDomain.GetAllUser());

                          
                return RequestResult<List<PostUserDto>>.CreateSuccessful(Users);

            }
            catch (Exception ex)
            {
                return RequestResult<List<PostUserDto>>.CreateError(ex.Message);
            }
        }
        /// <summary>
        ///     Obtiene la lista de usuarios del sitema.
        /// </summary>
        /// <author>Ariel Bejarano</author>    
        public async Task<RequestResult<List<PostUserDto>>> GetAssignmentByRol(Guid rol)
        {
            try
            {
                List<string> errorMessageValidations = new List<string>();
                var Users = _mapper.Map<List<Usuario>, List<PostUserDto>>(await _userDomain.GetUserAssignmentByRol(rol));


                return RequestResult<List<PostUserDto>>.CreateSuccessful(Users);

            }
            catch (Exception ex)
            {
                return RequestResult<List<PostUserDto>>.CreateError(ex.Message);
            }
        }
        /// <summary>
        ///     Obtiene la lista de usuarios del sitema.
        /// </summary>
        /// <author>Ariel Bejarano</author>    
        public async Task<RequestResult<List<PostUserDto>>> GetUserAssignmentById(Guid user)
        {
            try
            {
                List<string> errorMessageValidations = new List<string>();

                var Users = _mapper.Map<List<Usuario>, List<PostUserDto>>(await _userDomain.GetUserAssignmentById(user));


                return RequestResult<List<PostUserDto>>.CreateSuccessful(Users);

            }
            catch (Exception ex)
            {
                return RequestResult<List<PostUserDto>>.CreateError(ex.Message);
            }
        }
        /// <summary>
        ///     Guarda un usuario.
        /// </summary>
        /// <author>Ariel Bejarano</author>
        /// <param name="userDto">objeto para guardar orden de trabajo</param>
        public async Task<RequestResult<PostUserDto>> SaveUser(PostUserDto userDto)
        {
            try
            {
                List<string> errorMessageValidations = new List<string>();
                var user = _mapper.Map<PostUserDto, Usuario>(userDto);
                SaveUserValidations(ref errorMessageValidations,user);
                if(errorMessageValidations.Any())
                    return RequestResult<PostUserDto>.CreateUnsuccessful(errorMessageValidations);

                var findUser = await _userDomain.GetUser(user.NumeroDocumento);
                if (findUser != null)
                {                    
                    errorMessageValidations.Add(ResourceUserMsm.ExistUser);
                    return RequestResult<PostUserDto>.CreateUnsuccessful(errorMessageValidations);
                }
                //detereminar contraseña generica en el config o una tabla de variables genericas
                user.Contraseña = "Telcos2023";
                var hash = _hash.GetHash(user.Contraseña);
                user.Contraseña = hash.Hash;
                user.Salt = Convert.ToBase64String(hash.SaltHash);               
                _userDomain.SaveUser(user);
                return RequestResult<PostUserDto>.CreateSuccessful(userDto);

            }
            catch (Exception ex)
            {
                return RequestResult<PostUserDto>.CreateError(ex.Message);
            }
        }
        
        /// <summary>
        ///     Guarda un usuario.
        /// </summary>
        /// <author>Ariel Bejarano</author>
        /// <param name="userDto">objeto para guardar orden de trabajo</param>
        public async Task<RequestResult<PostUserDto>> UpdateUser(PostUserDto userDto)
        {
            try
            {
                List<string> errorMessageValidations = new List<string>();
                var user = _mapper.Map<PostUserDto, Usuario>(userDto);
                SaveUserValidations(ref errorMessageValidations, user);
                if (errorMessageValidations.Any())
                    return RequestResult<PostUserDto>.CreateUnsuccessful(errorMessageValidations);

                Usuario findUser = await _userDomain.GetUserById(user.ID);
                if (findUser == null)
                {
                    //Context.Entry(resultUpdate).CurrentValues.SetValues(recordAnestesia);
                    errorMessageValidations.Add(ResourceUserMsm.NotExistUser);
                    return RequestResult<PostUserDto>.CreateUnsuccessful(errorMessageValidations);
                }

               

                _userDomain.UpdateUser(findUser,user);
                return RequestResult<PostUserDto>.CreateSuccessful(userDto);

            }
            catch (Exception ex)
            {
                return RequestResult<PostUserDto>.CreateError(ex.Message);
            }
        }
        #region Private Methods
        /// <summary>
        ///     valida los datos para crear un usuario.
        /// </summary>
        /// <author>Ariel Bejarano</author>
        /// <param name="userDto">objeto para guardar orden de trabajo</param>
        private  void SaveUserValidations(ref List<string> errorMessageValidations, Usuario user)
        {
            if (string.IsNullOrEmpty(user.NumeroDocumento))
            {
                errorMessageValidations.Add(ResourceUserMsm.InvalidParameterDocument);
            }          
           
        }
        #endregion
    }
}

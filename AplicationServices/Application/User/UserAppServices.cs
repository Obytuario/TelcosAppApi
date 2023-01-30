using AplicationServices.Application.Contracts.User;
using AplicationServices.DTOs.Generics;
using AplicationServices.DTOs.User;
using AplicationServices.DTOs.workOrderManagement;
using AplicationServices.Helpers.HashResource;
using AutoMapper;
using DomainServices.Domain.Contracts.User;
using DomainServices.Domain.Contracts.WorkOrderManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelcosAppApi.DataAccess.Entities;

namespace AplicationServices.Application.User
{
    public class UserAppServices:IUserServices
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
        ///     Guarda un usuario.
        /// </summary>
        /// <author>Ariel Bejarano</author>
        /// <param name="userDto">objeto para guardar orden de trabajo</param>
        public async Task<RequestResult<PostUserDto>> SaveUser(PostUserDto userDto)
        {
            try
            {
                var user = _mapper.Map<PostUserDto, Usuario>(userDto);
                var hash = _hash.GetHash(userDto.ContrasenaDto);
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
    }
}

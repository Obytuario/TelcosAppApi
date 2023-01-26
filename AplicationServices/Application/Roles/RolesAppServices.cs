using TelcosAppApi.AplicationServices.Application.Contracts.Roles;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelcosAppApi.DataAccess.Entities;
using TelcosAppApi.DomainServices.Domain.Contracts.Roles;
using AplicationServices.DTOs.Generics;
using AutoMapper;


namespace TelcosAppApi.AplicationServices.Application.Roles
{

    public class RolesAppServices :IRolesServices
    {
        /// <summary>
        /// Instancia al servicio de Dominio
        /// </summary>
        private readonly IRolesDomain _rolesDomain;
        /// <summary>
        /// Mapper
        /// </summary>
        private readonly IMapper _mapper;
        public RolesAppServices(IRolesDomain rolesDomain, IMapper mapper)
        {
            _rolesDomain = rolesDomain;
            _mapper = mapper;
        }
        #region Method
        public async Task<RequestResult<List<GenericDto>>> GetRoles()
        {
            try
            {
                return RequestResult<List<GenericDto>>.CreateSuccessful(_mapper.Map<List<Rol>, List<GenericDto>>(await _rolesDomain.GetRoles()));
               
                
            }
            catch (Exception ex)
            {
                return RequestResult<List<GenericDto>>.CreateError(ex.Message);
            }
        }
        #endregion
    }
}

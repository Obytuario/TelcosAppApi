using AplicationServices.Application.Contracts.Cargos;
using AplicationServices.DTOs.Generics;
using AutoMapper;
using DomainServices.Domain.Contracts.cargos;
using DomainServices.Domain.Contracts.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelcosAppApi.DataAccess.Entities;

namespace AplicationServices.Application.Cargos
{
    public class CargosAppServices : ICargosServices
    {
        /// <summary>
        /// Instancia al servicio de Dominio
        /// </summary>
        private readonly ICargosDomain _cargosDomain;
        /// <summary>
        /// Mapper
        /// </summary>
        private readonly IMapper _mapper;
        public CargosAppServices(ICargosDomain cargosDomain, IMapper mapper)
        {
            _cargosDomain = cargosDomain;
            _mapper = mapper;
        }
        #region Method
        public async Task<RequestResult<List<GenericDto>>> GetCargos()
        {
            try
            {
                return RequestResult<List<GenericDto>>.CreateSuccessful(_mapper.Map<List<Cargo>, List<GenericDto>>(await _cargosDomain.GetCargos()));


            }
            catch (Exception ex)
            {
                return RequestResult<List<GenericDto>>.CreateError(ex.Message);
            }
        }
        #endregion
    }
}

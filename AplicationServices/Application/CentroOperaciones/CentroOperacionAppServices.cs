using AplicationServices.Application.Contracts.CentroOperaciones;
using AplicationServices.DTOs.Generics;
using AutoMapper;
using DomainServices.Domain.Contracts.cargos;

using DomainServices.Domain.Contracts.CentroOperaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelcosAppApi.DataAccess.Entities;

namespace AplicationServices.Application.CentroOperaciones
{
    public class CentroOperacionesAppServices: ICentroOperacionServices
    {
        /// <summary>
        /// Instancia al servicio de Dominio
        /// </summary>
        private readonly ICentroOperacionesDomain _centroOperacionDomain;
        /// <summary>
        /// Mapper
        /// </summary>
        private readonly IMapper _mapper;
        public CentroOperacionesAppServices(ICentroOperacionesDomain centroOperacionDomain, IMapper mapper)
        {
            _centroOperacionDomain = centroOperacionDomain;
            _mapper = mapper;
        }
        #region Method
        public async Task<RequestResult<List<GenericDto>>> GetCentroOperaciones()
        {
            try
            {
                return RequestResult<List<GenericDto>>.CreateSuccessful(_mapper.Map<List<CentroOperacion>, List<GenericDto>>(await _centroOperacionDomain.GetCentroOperaciones()));


            }
            catch (Exception ex)
            {
                return RequestResult<List<GenericDto>>.CreateError(ex.Message);
            }
        }
        #endregion
    }
}

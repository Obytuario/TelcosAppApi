using AplicationServices.Application.Contracts.Subscriber;
using AplicationServices.DTOs.Generics;
using AutoMapper;
using DomainServices.Domain.Contracts.cargos;
using DomainServices.Domain.Contracts.Subscriber;
using DomainServices.Domain.Subscriber;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelcosAppApi.DataAccess.Entities;

namespace AplicationServices.Application.Subscriber
{
    public class SubscriberAppServices : ISubscriberServices
    {
        /// <summary>
        /// Instancia al servicio de Dominio
        /// </summary>
        private readonly ISubscriberDomain _subscriberDomain;
        /// <summary>
        /// Mapper
        /// </summary>
        private readonly IMapper _mapper;
        public SubscriberAppServices(ISubscriberDomain subscriberDomain, IMapper mapper)
        {
            _subscriberDomain = subscriberDomain;
            _mapper = mapper;
        }

        #region Method
        public async Task<RequestResult<List<GenericDto>>> GetAllSubscriberType()
        {
            try
            {
                return RequestResult<List<GenericDto>>.CreateSuccessful(_mapper.Map<List<TipoSuscriptor>, List<GenericDto>>(await _subscriberDomain.GetAllSubscriberType()));


            }
            catch (Exception ex)
            {
                return RequestResult<List<GenericDto>>.CreateError(ex.Message);
            }
        }
        #endregion
    }
}

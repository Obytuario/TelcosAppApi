using AutoMapper;
using DomainServices.Domain.Contracts.WorkOrderManagement;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainServices.Domain.Contracts.WorkOrderFollowUp;
using AplicationServices.DTOs.Generics;
using AplicationServices.DTOs.workOrderManagement;
using DomainServices.Domain.WorkOrderManagement;
using TelcosAppApi.DataAccess.Entities;
using AplicationServices.DTOs.WorkOrderFollowUp;
using AplicationServices.Application.Contracts.WorkOrderFollowUp;

namespace AplicationServices.Application.WorkOrderFollowUp
{
    public class WorkOrderFollowUpServices: IWorkOrderFollowUpServices
    {
        /// <summary>
        /// Instancia al servicio de Dominio
        /// </summary>
        private readonly IWorkOrderFollowUpDomain _workOrderFollowUpDomain;
        /// <summary>
        /// Mapper
        /// </summary>
        private readonly IMapper _mapper;
        public WorkOrderFollowUpServices(IWorkOrderFollowUpDomain workOrderFollowUpDomain, IMapper mapper)
        {
            _workOrderFollowUpDomain = workOrderFollowUpDomain;
            _mapper = mapper;
        }
        /// <summary>
        ///     Obtiene la lista de ordenes de trabajo para seguimiento.
        /// </summary>
        /// <author>Ariel Bejarano</author>
        /// <param name="user">id del tecnico</param>
        public async Task<RequestResult<List<GetWorkOrderFollowUpDTO>>> GetWorkOrderFollowUp(PostWorkOrderFollowUpDTO filter)
        {
            try
            {
                return RequestResult<List<GetWorkOrderFollowUpDTO>>.CreateSuccessful(_mapper.Map<List<OrdenTrabajo>, List<GetWorkOrderFollowUpDTO>>(await _workOrderFollowUpDomain.GetWorkOrderFollowUp(filter.fechainicio, filter.fechaFin)));

            }
            catch (Exception ex)
            {
                return RequestResult<List<GetWorkOrderFollowUpDTO>>.CreateError(ex.Message);
            }
        }

    }
}

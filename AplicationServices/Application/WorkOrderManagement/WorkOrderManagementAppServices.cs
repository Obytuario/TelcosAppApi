using AplicationServices.Application.Contracts.WorkOrderManagement;
using AplicationServices.DTOs.workOrderManagement;
using AplicationServices.DTOs.Generics;
using AutoMapper;
using DomainServices.Domain.Contracts.WorkOrderManagement;
using DomainServices.Domain.WorkOrderManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelcosAppApi.AplicationServices.DTOs.Authentication;
using TelcosAppApi.DataAccess.Entities;



namespace AplicationServices.Application.WorkOrderManagement
{
    

    public class WorkOrderManagementAppServices: IWorkOrderManagementServices
    {
        /// <summary>
        /// Instancia al servicio de Dominio
        /// </summary>
        private readonly IWorkOrderManagementDomain _workOrderManagementDomain;
        /// <summary>
        /// Mapper
        /// </summary>
        private readonly IMapper _mapper;
        public WorkOrderManagementAppServices(IWorkOrderManagementDomain workOrderManagementDomain, IMapper mapper)
        {
            _workOrderManagementDomain = workOrderManagementDomain;
            _mapper = mapper;
        }
        #region Method
        /// <summary>
        ///     Obtiene la lista de ordenes de trabajo por tecnico.
        /// </summary>
        /// <author>Ariel Bejarano</author>
        /// <param name="user">id del tecnico</param>
        public async Task<RequestResult<List<GetWorkOrderManagementDTO>>> GetWorkOrderByUser(Guid? user)
        {
            try
            {
                return RequestResult<List<GetWorkOrderManagementDTO>>.CreateSuccessful(_mapper.Map<List<OrdenTrabajo>, List<GetWorkOrderManagementDTO>>(await _workOrderManagementDomain.GetWorkOrderByUser(user)));
                               
            }
            catch (Exception ex)
            {
                return RequestResult<List<GetWorkOrderManagementDTO>>.CreateError(ex.Message);
            }
        }
        /// <summary>
        ///     Guarda una orden de trabajo.
        /// </summary>
        /// <author>Ariel Bejarano</author>
        /// <param name="workOrder">objeto para guardar orden de trabajo</param>
        public async Task<RequestResult<PostWorkOrderManagementDTO>> SaveWorkOrder(PostWorkOrderManagementDTO workOrder)
        {
            try
            {
                var ordenTrabajo = _mapper.Map<PostWorkOrderManagementDTO, OrdenTrabajo>(workOrder);
                _workOrderManagementDomain.SaveWorkOrder(ordenTrabajo);                
                return RequestResult<PostWorkOrderManagementDTO>.CreateSuccessful(workOrder);

            }
            catch (Exception ex)
            {
                return RequestResult<PostWorkOrderManagementDTO>.CreateError(ex.Message);
            }
        }
        #endregion


    }
}

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
using AplicationServices.DTOs.User;
using AplicationServices.Helpers.TextResorce;

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
        
        /// <summary>
        ///     Obtiene la lista de ordenes de trabajo para seguimiento.
        /// </summary>
        /// <author>Ariel Bejarano</author>
        /// <param name="user">id del tecnico</param>
        public async Task<RequestResult<List<DetailWorkOrderFollowequipment>>> GetDetailEquipmentByOrder(Guid order)
        {
            try
            {
                return RequestResult<List<DetailWorkOrderFollowequipment>>.CreateSuccessful(_mapper.Map<List<DetalleEquipoOrdenTrabajo>, List<DetailWorkOrderFollowequipment>>(await _workOrderFollowUpDomain.GetDetailEquipmentByOrder(order)));
            }
            catch (Exception ex)
            {
                return RequestResult<List<DetailWorkOrderFollowequipment>>.CreateError(ex.Message);
            }
        }
        /// <summary>
        ///     Obtiene el detalle del material segun uan orden de trabajo
        /// </summary>
        /// <author>Ariel Bejarano</author>
        /// <param name="user">id del tecnico</param>
        public async Task<RequestResult<List<DetailWorkOrderFollowMaterial>>> GetDetailMaterialByOrder(Guid order)
        {
            try
            {
                return RequestResult<List<DetailWorkOrderFollowMaterial>>.CreateSuccessful(_mapper.Map<List<DetalleMaterialOrdenTrabajo>, List<DetailWorkOrderFollowMaterial>>(await _workOrderFollowUpDomain.GetDetailMaterialByOrder(order)));
            }
            catch (Exception ex)
            {
                return RequestResult<List<DetailWorkOrderFollowMaterial>>.CreateError(ex.Message);
            }
        }

        /// <summary>
        ///     Obtiene el los movimientos de equipos
        /// </summary>
        /// <author>Ariel Bejarano</author>
        /// <param name="user">id del tecnico</param>
        public async Task<RequestResult<List<GenericDto>>> GetAllMovimientoEquipment()
        {
            try
            {
                return RequestResult<List<GenericDto>>.CreateSuccessful(_mapper.Map<List<MovimientoEquipo>, List<GenericDto>>(await _workOrderFollowUpDomain.GetAllMovimientoEquipment()));
            }
            catch (Exception ex)
            {
                return RequestResult<List<GenericDto>>.CreateError(ex.Message);
            }
        }
        /// <summary>
        ///     Actuaiza detalle equipos de una orden
        /// </summary>
        /// <author>Ariel Bejarano</author>
        /// <param name="detail">obejteo con los datos</param>
        public async Task<RequestResult<DetailWorkOrderFollowequipment>> UpdateDetailEquipmentFollow(DetailWorkOrderFollowequipment detail)
        {
            try
            {
                List<string> errorMessageValidations = new List<string>();
                var detalleEquipo = _mapper.Map<DetailWorkOrderFollowequipment, DetalleEquipoOrdenTrabajo>(detail);
                SaveWorkFollowEquipmentValidations(ref errorMessageValidations, detalleEquipo);
                if (errorMessageValidations.Any())
                    return RequestResult<DetailWorkOrderFollowequipment>.CreateUnsuccessful(errorMessageValidations);

               

                _workOrderFollowUpDomain.UpdateDetailEquipment(detalleEquipo);
                return RequestResult<DetailWorkOrderFollowequipment>.CreateSuccessful(detail);

            }
            catch (Exception ex)
            {
                return RequestResult<DetailWorkOrderFollowequipment>.CreateError(ex.Message);
            }
        }
        /// <summary>
        ///     Actuaiza detalle equipos de una orden
        /// </summary>
        /// <author>Ariel Bejarano</author>
        /// <param name="detail">obejteo con los datos</param>
        public async Task<RequestResult<DetailWorkOrderFollowMaterial>> UpdateDetailMaterialFollow(DetailWorkOrderFollowMaterial detail)
        {
            try
            {
                List<string> errorMessageValidations = new List<string>();
                var detalleMaterial = _mapper.Map<DetailWorkOrderFollowMaterial, DetalleMaterialOrdenTrabajo>(detail);
                SaveWorkFollowMaterialValidations(ref errorMessageValidations, detalleMaterial);
                if (errorMessageValidations.Any())
                    return RequestResult<DetailWorkOrderFollowMaterial>.CreateUnsuccessful(errorMessageValidations);

                _workOrderFollowUpDomain.UpdateDetailMaterial(detalleMaterial);
                
                return RequestResult<DetailWorkOrderFollowMaterial>.CreateSuccessful(detail);

            }
            catch (Exception ex)
            {
                return RequestResult<DetailWorkOrderFollowMaterial>.CreateError(ex.Message);
            }
        }

        #region Private Methods
        /// <summary>
        ///     valida los datos para crear un usuario.
        /// </summary>
        /// <author>Ariel Bejarano</author>
        /// <param name="userDto">objeto para guardar orden de trabajo</param>
        private void SaveWorkFollowEquipmentValidations(ref List<string> errorMessageValidations, DetalleEquipoOrdenTrabajo detail)
        {
            if (string.IsNullOrEmpty(detail.Serial))
            {
                errorMessageValidations.Add(ResourceUserMsm.InvalidParameterDocument);
            }

        }
        private void SaveWorkFollowMaterialValidations(ref List<string> errorMessageValidations, DetalleMaterialOrdenTrabajo detail)
        {
            //if (string.IsNullOrEmpty(detail.Cantidad))
            //{
            //    errorMessageValidations.Add(ResourceUserMsm.InvalidParameterDocument);
            //}

        }
        #endregion

    }
}

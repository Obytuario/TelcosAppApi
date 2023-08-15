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
using TelcosAppApi.DataAccess.Entities;
using AplicationServices.Helpers.TextResorce;
using Microsoft.EntityFrameworkCore.Metadata;
using AplicationServices.Application.Contracts.Carpetas;

namespace AplicationServices.Application.WorkOrderManagement
{
    

    public class WorkOrderManagementAppServices: IWorkOrderManagementServices
    {

        #region CONST

        public readonly string CODIGO_ESTADO_ENPROCESO = "ENPR";
        public readonly string CODIGO_ESTADO_EXITOSA = "EXIT";
        public readonly string CODIGO_ESTADO_CANCELADA = "RAZO";

        #endregion

        /// <summary>
        /// Instancia al servicio de Dominio
        /// </summary>
        private readonly IWorkOrderManagementDomain _workOrderManagementDomain;
        private readonly ICarpetasServices _carpetasServices;
        /// <summary>
        /// Mapper
        /// </summary>
        private readonly IMapper _mapper;
        public WorkOrderManagementAppServices(IWorkOrderManagementDomain workOrderManagementDomain, IMapper mapper, ICarpetasServices carpetasServices )
        {
            _workOrderManagementDomain = workOrderManagementDomain;
            _carpetasServices = carpetasServices;
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
                var result = await _workOrderManagementDomain.GetWorkOrderByUser(user);               
                return RequestResult<List<GetWorkOrderManagementDTO>>.CreateSuccessful(_mapper.Map<List<OrdenTrabajo>, List<GetWorkOrderManagementDTO>>(result));
                               
            }
            catch (Exception ex)
            {
                return RequestResult<List<GetWorkOrderManagementDTO>>.CreateError(ex.Message);
            }
        }
        /// <summary>
        ///     Obtiene la lista de tipo de suscriptores
        /// </summary>
        /// <author>Ariel Bejarano</author>
        /// <param name="user">id del tecnico</param>
        public async Task<RequestResult<List<GenericDto>>> GetSubscriberType()
        {
            try
            {
                return RequestResult<List<GenericDto>>.CreateSuccessful(_mapper.Map<List<TipoSuscriptor>, List<GenericDto>>(await _workOrderManagementDomain.GetSubscriberType()));

            }
            catch (Exception ex)
            {
                return RequestResult<List<GenericDto>>.CreateError(ex.Message);
            }
        }
        /// <summary>
        ///     Obtiene la lista de estados de ordenes de trabajo
        /// </summary>
        /// <author>Ariel Bejarano</author>       
        public async Task<RequestResult<List<GenericDto>>> GetWorkOrderStatus()
        {
            try
            {
                return RequestResult<List<GenericDto>>.CreateSuccessful(_mapper.Map<List<EstadoOrdenTrabajo>, List<GenericDto>>(await _workOrderManagementDomain.GetWorkOrderStatus()));

            }
            catch (Exception ex)
            {
                return RequestResult<List<GenericDto>>.CreateError(ex.Message);
            }
        }
        /// <summary>
        ///     Obtiene la lista de motivos de consulta
        /// </summary>
        /// <author>Ariel Bejarano</author>       
        public async Task<RequestResult<List<GenericDto>>> GetWorkOrderReasonCancel()
        {
            try
            {
                return RequestResult<List<GenericDto>>.CreateSuccessful(_mapper.Map<List<MotivoCancelacionOrden>, List<GenericDto>>(await _workOrderManagementDomain.GetWorkOrderReasonCancel()));

            }
            catch (Exception ex)
            {
                return RequestResult<List<GenericDto>>.CreateError(ex.Message);
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
                List<string> errorMessageValidations = new List<string>();

                Guid? idOrdenTrabajo = _workOrderManagementDomain.GetWorkOrderByNumber(workOrder.NumeroOrdenDto).Result?.ID;

                if (idOrdenTrabajo != null)
                {
                    errorMessageValidations.Add(ResourceUserMsm.ExistingOrder);
                    return RequestResult<PostWorkOrderManagementDTO>.CreateUnsuccessful(errorMessageValidations);
                }

                OrdenTrabajo ordenTrabajo = _mapper.Map<PostWorkOrderManagementDTO, OrdenTrabajo>(workOrder);

                ordenTrabajo.EstadoOrden = _workOrderManagementDomain.GetWorkOrderStatus().Result.Find(f => f.Codigo == CODIGO_ESTADO_ENPROCESO).ID;
                _workOrderManagementDomain.SaveWorkOrder(ordenTrabajo);                

                return RequestResult<PostWorkOrderManagementDTO>.CreateSuccessful(workOrder);

            }
            catch (Exception ex)
            {
                return RequestResult<PostWorkOrderManagementDTO>.CreateError(ex.Message);
            }
        }


        /// <summary>
        /// Actualiza y guarda la informacion de una gestión de orden de trabajo
        /// </summary>
        /// <param name="workOrder"></param>
        /// <returns></returns>
        /// <author>Diego MOlina</author>
        public async Task<RequestResult<Guid>> UpdateManageWorkOrder(UpdateWorkOrderManagementDTO workOrder, string pathServer)
        {
            try
            {
                
                OrdenTrabajo ordenTrabajo = _workOrderManagementDomain.GetWorkOrderById(workOrder.IdWorkOrder).Result;
                ordenTrabajo.EstadoOrden = _workOrderManagementDomain.GetWorkOrderStatus().Result.Find(f => f.Codigo == CODIGO_ESTADO_EXITOSA).ID;
                ordenTrabajo.TecnicoAuxiliar = workOrder.IdAssitant;

                ICollection<DetalleEquipoOrdenTrabajo> detalleEquipoOrdenTrabajo = _mapper.Map<List<EquiptmentDto>, ICollection<DetalleEquipoOrdenTrabajo>>(workOrder.Supplies.Equiptments);
                ICollection<DetalleMaterialOrdenTrabajo>  detalleMaterialOrdenTrabajo = _mapper.Map<List<MaterialDto>, ICollection<DetalleMaterialOrdenTrabajo>>(workOrder.Supplies.Materials);

                //workOrder.Activitys.ForEach(a => {

                //    detalleEquipoOrdenTrabajo.ToList().ForEach(e => {
                //        ParamEquipoActividad paramEquipoActividad = new ParamEquipoActividad();
                //        if  (!(paramEquipoActividad.Actividad == Guid.Parse(a)))
                //        {

                //        }
                //    });

                //});

                if (detalleEquipoOrdenTrabajo.Count() > 0)
                {
                    detalleEquipoOrdenTrabajo.ToList().ForEach(x => x.UsuarioRegistra = workOrder.IdUser);
                    detalleEquipoOrdenTrabajo.ToList().ForEach(x => x.OrdenTrabajo = workOrder.IdWorkOrder);
                    _workOrderManagementDomain.SaveDetalleEquipoOrdenTrabajo(detalleEquipoOrdenTrabajo);
                }
                

                if (detalleMaterialOrdenTrabajo.Count() > 0)
                {
                    detalleMaterialOrdenTrabajo.ToList().ForEach(x => x.UsuarioRegistra = workOrder.IdUser);
                    detalleMaterialOrdenTrabajo.ToList().ForEach(x => x.OrdenTrabajo = workOrder.IdWorkOrder);
                    _workOrderManagementDomain.SaveDetalleMaterialOrdenTrabajo(detalleMaterialOrdenTrabajo);
                }

                if (workOrder.Photos.Count() > 0)
                {
                    List<DetalleImagenOrdenTrabajo> detalleImagenOrdenTrabajo = new List<DetalleImagenOrdenTrabajo>();

                    workOrder.Photos.ForEach(async x => {
                        DetalleImagenOrdenTrabajo detalleImagenOrdenTrabajoDto = _mapper.Map<ImageDto, DetalleImagenOrdenTrabajo>(x);
                        detalleImagenOrdenTrabajoDto.UrlImagen = (await _carpetasServices.UploadImageByWorkOrder(x, pathServer)).Result;
                        detalleImagenOrdenTrabajoDto.OrdenTrabajo = workOrder.IdWorkOrder;
                        detalleImagenOrdenTrabajo.Add(detalleImagenOrdenTrabajoDto);
                    }) ;  
                 
                    _workOrderManagementDomain.SaveDetalleImagenOrdenTrabajo(detalleImagenOrdenTrabajo);
                }




                _workOrderManagementDomain.SaveChanges();

                return RequestResult<Guid>.CreateSuccessful(workOrder.IdWorkOrder);

            }
            catch (Exception ex)
            {
                return RequestResult<Guid>.CreateError(ex.Message);
            }
        }
        /// <summary>
        /// Actualiza y guarda la informacion de una gestión de orden de trabajo
        /// </summary>
        /// <param name="workOrder"></param>
        /// <returns></returns>
        /// <author>Ariel Bejarano</author>
        public async Task<RequestResult<Guid>> CancelWorkOrder(CancelWorkOrderManagementDTO workOrder)
        {
            try
            {

                OrdenTrabajo ordenTrabajo = _workOrderManagementDomain.GetWorkOrderById(workOrder.IdWorkOrder).Result;
                ordenTrabajo.EstadoOrden = _workOrderManagementDomain.GetWorkOrderStatus().Result.Find(f => f.Codigo == CODIGO_ESTADO_CANCELADA).ID;
                

                DetalleCancelacionOrden detalleCancelacionOrden = _mapper.Map<CancelWorkOrderManagementDTO, DetalleCancelacionOrden>(workOrder);
                detalleCancelacionOrden.OrdenTrabajo.Add(ordenTrabajo);

                _workOrderManagementDomain.saveDetalleCancelacionOrden(detalleCancelacionOrden);

                _workOrderManagementDomain.SaveChanges();

                return RequestResult<Guid>.CreateSuccessful(workOrder.IdWorkOrder);

            }
            catch (Exception ex)
            {
                return RequestResult<Guid>.CreateError(ex.Message);
            }
        }

        #endregion


    }
}

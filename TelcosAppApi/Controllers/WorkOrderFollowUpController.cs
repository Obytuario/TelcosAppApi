using AplicationServices.Application.Contracts.Roles;
using AplicationServices.Application.Contracts.Subscriber;
using AplicationServices.Application.Contracts.WorkOrderFollowUp;
using AplicationServices.Application.Contracts.WorkOrderManagement;
using AplicationServices.DTOs.Generics;
using AplicationServices.DTOs.WorkOrderFollowUp;
using AplicationServices.DTOs.workOrderManagement;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TelcosAppApi.Controllers
{
    [ApiController]
    [Route("Api/WorkOrderFollowUp")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WorkOrderFollowUpController : ControllerBase
    {
        #region Fiedls

        /// <summary>
        /// Instancia al servicio de aplicación
        /// </summary>
        private readonly IWorkOrderFollowUpServices _WorkOrderFollowUpServices;


        #endregion Fiedls

        public WorkOrderFollowUpController(IWorkOrderFollowUpServices WorkOrderFollowUpServices)
        {
            _WorkOrderFollowUpServices = WorkOrderFollowUpServices;
        }
        /// <summary>
        /// Consulta las ordenes del dia de un tecnico.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <author>Ariel Bejarano</author>
        [HttpGet("GetDetailEquipmentByOrder")]
        public async Task<RequestResult<List<DetailWorkOrderFollowequipment>>> GetDetailEquipmentByOrder(Guid order)
        {

            return await _WorkOrderFollowUpServices.GetDetailEquipmentByOrder(order);
        }
        /// <summary>
        /// Consulta las ordenes del dia de un tecnico.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <author>Ariel Bejarano</author>
        [HttpGet("GetDetailMaterialByOrder")]
        public async Task<RequestResult<List<DetailWorkOrderFollowMaterial>>> GetDetailMaterialByOrder(Guid order)
        {

            return await _WorkOrderFollowUpServices.GetDetailMaterialByOrder(order);
        }
        /// <summary>
        /// Obtiene todos los movimientos de equipo
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        /// <author>Ariel Bejarano</author>
        [HttpGet("GetAllMovimientoEquipment")]
        public async Task<RequestResult<List<GenericDto>>> GetAllMovimientoEquipment()
        {
            return await _WorkOrderFollowUpServices.GetAllMovimientoEquipment();
        }

        /// <summary>
        /// Consulta las ordenes del dia de un tecnico.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <author>Ariel Bejarano</author>
        /// 
        [HttpPost("GetWorkOrderFollow")]
        public async Task<RequestResult<List<GetWorkOrderFollowUpDTO>>> GetWorkOrderByUser(PostWorkOrderFollowUpDTO filter)
        {
            return await _WorkOrderFollowUpServices.GetWorkOrderFollowUp(filter);
        }
        /// <summary>
        /// Actualiza detalle de equipos.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <author>Ariel Bejarano</author>
        /// 
        [HttpPost("UpdateDetailEquipmentFollow")]
        public async Task<RequestResult<DetailWorkOrderFollowequipment>> UpdateDetailEquipmentFollow(DetailWorkOrderFollowequipment detail)
        {
            return await _WorkOrderFollowUpServices.UpdateDetailEquipmentFollow(detail);
        }
        /// <summary>
        /// Actualiza detalle de equipos.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <author>Ariel Bejarano</author>
        /// 
        [HttpPost("UpdateDetailMaterialFollow")]
        public async Task<RequestResult<DetailWorkOrderFollowMaterial>> UpdateDetailMaterialFollow(DetailWorkOrderFollowMaterial detail)
        {
            return await _WorkOrderFollowUpServices.UpdateDetailMaterialFollow(detail);
        }
        /// <summary>
        /// Actualiza detalle de equipos.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <author>Ariel Bejarano</author>
        /// 
        [HttpPost("DeleteDetailEquipmentFollow")]
        public async Task<RequestResult<DetailWorkOrderFollowequipment>> DeleteDetailEquipmentFollow(DetailWorkOrderFollowequipment detail)
        {
            return await _WorkOrderFollowUpServices.DeleteDetailEquipmentFollow(detail);
        }
        /// <summary>
        /// Actualiza detalle de equipos.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <author>Ariel Bejarano</author>
        /// 
        [HttpPost("DeleteDetailMaterialFollow")]
        public async Task<RequestResult<DetailWorkOrderFollowMaterial>> DeleteDetailMaterialFollow(DetailWorkOrderFollowMaterial detail)
        {
            return await _WorkOrderFollowUpServices.DeleteDetailMaterialFollow(detail);
        }

        /// <summary>
        /// Obtiene todos las actividades
        /// </summary>
        /// <returns></returns>
        /// <author>Diego Molina</author>
        [HttpGet("GetActivity")]
        public async Task<RequestResult<List<GenericDto>>> GetActivity()
        {
            return await _WorkOrderFollowUpServices.GetActivity();
        }

        /// <summary>
        /// Obtiene todos los tipos de foto
        /// </summary>
        /// <returns></returns>
        /// <author>Diego Molina</author>
        [HttpGet("GetPhotoType")]
        public async Task<RequestResult<List<GenericDto>>> GetPhotoType()
        {
            return await _WorkOrderFollowUpServices.GetPhotoType();
        }

    }
}

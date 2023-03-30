using AplicationServices.Application.Contracts.WorkOrderManagement;
using AplicationServices.DTOs.Generics;
using AplicationServices.DTOs.workOrderManagement;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TelcosAppApi.DataAccess.Entities;

namespace TelcosAppApi.Controllers
{
    [ApiController]
    [Route("Api/WorkOrderManagement")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WorkOrderManagementController : ControllerBase
    {
        #region Fiedls

        /// <summary>
        /// Instancia al servicio de aplicación
        /// </summary>
        private readonly IWorkOrderManagementServices _WorkOrderManagementServices;


        #endregion Fiedls
        public WorkOrderManagementController(IWorkOrderManagementServices WorkOrderManagementServices)
        {
            _WorkOrderManagementServices = WorkOrderManagementServices;
        }
        /// <summary>
        /// Consulta las ordenes del dia de un tecnico.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <author>Ariel Bejarano</author>
        [HttpGet("GetWorkOrderByUser")]
        public async Task<RequestResult<List<GetWorkOrderManagementDTO>>> GetWorkOrderByUser(Guid? user)
        {

            return await _WorkOrderManagementServices.GetWorkOrderByUser(user);
        }
        /// <summary>
        /// Consulta los tipos de suscriptor.
        /// </summary>        
        /// <returns></returns>
        /// <author>Ariel Bejarano</author>
        [HttpGet("GetSubscriberType")]
        public async Task<RequestResult<List<GenericDto>>> GetSubscriberType()
        {
            return await _WorkOrderManagementServices.GetSubscriberType();
        }
        /// <summary>
        /// Consulta los tipos de ordenes de trabajo.
        /// </summary>        
        /// <returns></returns>
        /// <author>Ariel Bejarano</author>
        [HttpGet("GetWorkOrderStatus")]
        public async Task<RequestResult<List<GenericDto>>> GetWorkOrderStatus()
        {
            return await _WorkOrderManagementServices.GetWorkOrderStatus();
        }
        
        /// <summary>
        /// Guarda la informacion de una orden de trabajo
        /// </summary>
        /// <param name="workOrder"></param>
        /// <returns></returns>
        /// <author>Ariel Bejarano</author>
        [HttpPost("SaveWorkOrder")]
        public async Task<RequestResult<PostWorkOrderManagementDTO>> SaveWorkOrder(PostWorkOrderManagementDTO workOrder)
        {
            return await _WorkOrderManagementServices.SaveWorkOrder(workOrder);
        }

        /// <summary>
        /// Actualiza y guarda la informacion de una gestión de orden de trabajo
        /// </summary>
        /// <param name="workOrder"></param>
        /// <returns></returns>
        /// <author>Diego MOlina</author>
        [HttpPost("UpdateManageWorkOrder")]
        public async Task<RequestResult<Guid>> UpdateManageWorkOrder(UpdateWorkOrderManagementDTO workOrder)
        {
            return await _WorkOrderManagementServices.UpdateManageWorkOrder(workOrder);
        }

    }
}

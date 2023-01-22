using AplicationServices.Application.Contracts.WorkOrderManagement;
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

        [HttpGet]
        public async Task<ActionResult<List<GetWorkOrderManagementDTO>>> GetWorkOrderByUser(Guid? user)
        {

            return await _WorkOrderManagementServices.GetWorkOrderByUser(user);
        }
        [HttpPost]
        public async Task<ActionResult<PostWorkOrderManagementDTO>> GetWorkOrderByUser(PostWorkOrderManagementDTO workOrder)
        {
            return await _WorkOrderManagementServices.SaveWorkOrder(workOrder);
        }
        
    }
}

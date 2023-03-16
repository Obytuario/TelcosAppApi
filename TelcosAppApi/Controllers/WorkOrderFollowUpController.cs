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
        [HttpGet("GetWorkOrderFollow")]
        public async Task<RequestResult<List<GetWorkOrderFollowUpDTO>>> GetWorkOrderByUser(PostWorkOrderFollowUpDTO filter)
        {

            return await _WorkOrderFollowUpServices.GetWorkOrderFollowUp(filter);
        }
    }
}

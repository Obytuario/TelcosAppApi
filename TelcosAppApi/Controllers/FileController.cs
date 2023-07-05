using AplicationServices.Application.Contracts.Carpetas;
using AplicationServices.Application.Contracts.Roles;
using AplicationServices.DTOs.Generics;
using AplicationServices.DTOs.WorkOrderFollowUp;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TelcosAppApi.Controllers
{
    [ApiController]
    [Route("Api/Files")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FileController : ControllerBase
    {
        #region Fiedls

        /// <summary>
        /// Instancia al servicio de aplicación
        /// </summary>
        private readonly ICarpetasServices _carpetasServices;


        #endregion Fiedls
        public FileController(ICarpetasServices carpetasServices)
        {
            _carpetasServices = carpetasServices;
        }

        /// <summary>
        /// Obtiene todos los roles del sistema
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        /// <author>Ariel Bejarano</author>
        [HttpGet("GetAllFile")]
        public async Task<RequestResult<List<GenericDto>>> Get()
        {
            return await _carpetasServices.GetFiles();
        }
        /// <summary>
        /// Consulta las equipos agrupados por actividad filtrados por carpeta
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <author>Ariel Bejarano</author>
        [HttpGet("GetActyvitiEquipmentByFile")]
        public async Task<RequestResult<List<paramGenericDto>>> GetActyvitiEquipmentByFile(Guid file)
        {
            return await _carpetasServices.GetActyvitiEquipmentByFile(file);
        }
        /// <summary>
        /// Consulta las equipos agrupados por actividad filtrados por carpeta
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <author>Ariel Bejarano</author>
        [HttpGet("GetActyvitiMaterialByFile")]
        public async Task<RequestResult<List<paramGenericDto>>> GetActyvitiMaterialByFile(Guid file)
        {
            return await _carpetasServices.GetActyvitiMaterialByFile(file);
        }
        /// <summary>
        /// Consulta las equipos por actividad
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <author>Ariel Bejarano</author>
        [HttpGet("GetEquipmentByActivity")]
        public async Task<RequestResult<List<paramGenericDto>>> GetEquipmentByActivity(Guid activity)
        {
            return await _carpetasServices.GetEquipmentByActivity(activity);
        }
        /// <summary>
        /// Consulta los materiales por actividad
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <author>Ariel Bejarano</author>
        [HttpGet("GetMaterialByActivity")]
        public async Task<RequestResult<List<paramGenericDto>>> GetMaterialByActivity(Guid activity)
        {
            return await _carpetasServices.GetMaterialByActivity(activity);
        }

        /// <summary>
        /// Consulta imagenes por id
        /// </summary>
        /// <param name="workOrder"></param>
        /// <returns></returns>
        /// <author>Ariel Bejarano</author>
        [HttpGet("GetImageById")]
        public async Task<RequestResult<List<imageGenericDto>>> GetImageById(Guid workOrder)
        {
            return await _carpetasServices.GetImageById(workOrder);
        }
    }
}

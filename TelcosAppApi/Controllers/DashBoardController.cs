using AplicationServices.Application.Contracts.Cargos;
using AplicationServices.Application.Contracts.DashBoard;
using AplicationServices.DTOs.Generics;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TelcosAppApi.AplicationServices.Application.Contracts.Authentication;
using TelcosAppApi.AplicationServices.DTOs.Authentication;

namespace TelcosAppApi.Controllers
{
    [ApiController]
    [Route("Api/DashBoard")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DashBoardController : ControllerBase
    {
        /// <summary>
        /// Instancia al servicio de aplicación
        /// </summary>
        private readonly IDashBoardServices _dashBoardServices;
        //private readonly IMapper _mapper;
        public DashBoardController(IDashBoardServices dashBoardServices)
        {
            _dashBoardServices = dashBoardServices; 
        }


        /// <summary>
        /// Obtiene todos los roles del sistema
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        /// <author>Ariel Bejarano</author>
        [HttpGet("GetDaskBoard")]
        public async Task<RequestResult<DashBoardOperationsDTO>> GetDaskBoard(string code)
        {
            //definir codigo segun dasboard a cargar.
            return await _dashBoardServices.GetDashBoard(code);
        }
    }
}

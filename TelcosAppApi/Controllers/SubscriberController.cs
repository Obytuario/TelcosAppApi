using AplicationServices.Application.Contracts.Subscriber;
using AplicationServices.DTOs.Generics;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TelcosAppApi.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SubscriberController : ControllerBase
    {
        /// <summary>
        /// Instancia al servicio de aplicación
        /// </summary>
        private readonly ISubscriberServices _subscriberServices;



        public SubscriberController(ISubscriberServices subscriberServices)
        {
            _subscriberServices = subscriberServices;
        }

        /// <summary>
        /// Obtiene todos los tipos de subcritores
        /// </summary>
        /// <returns></returns>
        /// <author>Diego Molina</author>
        [HttpGet("GetAllSubscriberType")]
        public async Task<RequestResult<List<GenericDto>>> GetAllSubscriberType()
        {
            return await _subscriberServices.GetAllSubscriberType();
        }
    }
}

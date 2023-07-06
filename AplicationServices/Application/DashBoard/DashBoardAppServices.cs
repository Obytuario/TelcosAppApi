using AplicationServices.Application.Contracts.DashBoard;
using AplicationServices.Application.Contracts.Location;
using AplicationServices.DTOs.Generics;
using AplicationServices.DTOs.Location;
using AplicationServices.Helpers.TextResorce;
using AutoMapper;
using DomainServices.Domain.Contracts.Authentication;
using DomainServices.Domain.Contracts.DashBoard;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TelcosAppApi.AplicationServices.DTOs.Authentication;
using TelcosAppApi.DataAccess.Entities;

namespace AplicationServices.Application.DashBoard
{
   
    public class DashBoardAppServices: IDashBoardServices
    {
        /// <summary>
        /// Instancia al servicio de Dominio
        /// </summary>
        private readonly IDashBoardDomain _dashBoardDomain;
        private readonly ILocationServices _LocationServices;
        /// <summary>
        /// Mapper
        /// </summary>
        private readonly IMapper _mapper;
        public DashBoardAppServices(IDashBoardDomain dashBoardDomain,IMapper mapper,ILocationServices locationServices)
        {
            _dashBoardDomain = dashBoardDomain;
            _mapper = mapper;   
            _LocationServices = locationServices;
        }
        #region Method
        public async Task<RequestResult<DashBoardOperationsDTO>> GetDashBoard(string code)
        {
            try
            {
                DashBoardOperationsDTO dashBoardOperationsDTO = new DashBoardOperationsDTO();
                dashBoardOperationsDTO.Gauge = new DashBoardGaugeDTO();
                dashBoardOperationsDTO.Cards = new List<DashBoardDTO>();

                DashBoardDTO dashBoardDTO;
                List<DashBoardDTO> dashBoardDTOs = new List<DashBoardDTO>();
                List<OrdenTrabajo> listOrdenTrabajo = await _dashBoardDomain.GetDashBoard();
                List<Usuario> userActive = await _dashBoardDomain.GetUserActiveLocation();
                dashBoardOperationsDTO.Gauge.Max = userActive.Count();
                dashBoardOperationsDTO.Gauge.Current = userActive.Where(x => x.UbicacionUsuario.Any(u => u.FechaHora.Date.Equals(DateTime.Now.Date))).Count();

                /*primera card de total de ordenes gestionadas*/
                dashBoardDTO = new DashBoardDTO();
                dashBoardDTO.Items = listOrdenTrabajo.Count();
                dashBoardDTO.Progres = await GenerateProgresCard(listOrdenTrabajo.Count(), listOrdenTrabajo.Where(f => !f.EstadoOrdenNavigation.Codigo.Equals("ENPR")).Count());
                dashBoardOperationsDTO.Cards.Add(dashBoardDTO);

                /*segunda card de total de ordenes exitosas o finalizadas*/
                dashBoardDTO = new DashBoardDTO();
                dashBoardDTO.Items = listOrdenTrabajo.Where(f => f.EstadoOrdenNavigation.Codigo.Equals("EXIT")).Count();
                dashBoardDTO.Progres = await GenerateProgresCard(listOrdenTrabajo.Count(), listOrdenTrabajo.Where(f => f.EstadoOrdenNavigation.Codigo.Equals("EXIT")).Count());
                dashBoardOperationsDTO.Cards.Add(dashBoardDTO);

                /*segunda card de total de ordenes Razonadas*/
                dashBoardDTO = new DashBoardDTO();
                dashBoardDTO.Items = listOrdenTrabajo.Where(f => f.EstadoOrdenNavigation.Codigo.Equals("RAZO")).Count();
                dashBoardDTO.Progres = await GenerateProgresCard(listOrdenTrabajo.Count(), listOrdenTrabajo.Where(f => f.EstadoOrdenNavigation.Codigo.Equals("RAZO")).Count());
                dashBoardOperationsDTO.Cards.Add(dashBoardDTO);



                return RequestResult<DashBoardOperationsDTO>.CreateSuccessful(dashBoardOperationsDTO);


            }
            catch (Exception ex)
            {
                return RequestResult<DashBoardOperationsDTO>.CreateError(ex.Message);
            }
        }
        #endregion
        #region Private Methods
       
        /// <summary>
        ///     calcul progreso dash board.
        /// </summary>
        /// <author>Ariel Bejarano</author>
        /// <param name="userDto">objeto para guardar orden de trabajo</param>
        private async Task<int> GenerateProgresCard(int items, int itemsAvance)
        {
            if(items.Equals(0) && itemsAvance.Equals(0))
                return 0;

            return  (itemsAvance * 100) / items;
        }
        #endregion
    }
}

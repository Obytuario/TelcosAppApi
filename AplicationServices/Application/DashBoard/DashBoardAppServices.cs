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
        /// <summary>
        /// Mapper
        /// </summary>
        private readonly IMapper _mapper;
        public DashBoardAppServices(IDashBoardDomain dashBoardDomain,IMapper mapper)
        {
            _dashBoardDomain = dashBoardDomain;
            _mapper = mapper;   
        }
        #region Method
        public async Task<RequestResult<List<DashBoardDTO>>> GetDashBoard(string code)
        {
            try
            {
                DashBoardDTO dashBoardDTO;
                List<DashBoardDTO> dashBoardDTOs = new List<DashBoardDTO>();
                List<OrdenTrabajo> listOrdenTrabajo = await _dashBoardDomain.GetDashBoard();

                /*primera card de total de ordenes gestionadas*/
                dashBoardDTO = new DashBoardDTO();
                dashBoardDTO.Items = listOrdenTrabajo.Count();
                dashBoardDTO.Progres = await GenerateProgresCard(listOrdenTrabajo.Count(), listOrdenTrabajo.Where(f => !f.EstadoOrdenNavigation.Codigo.Equals("ENPR")).Count());
                dashBoardDTOs.Add(dashBoardDTO);

                /*segunda card de total de ordenes exitosas o finalizadas*/
                dashBoardDTO = new DashBoardDTO();
                dashBoardDTO.Items = listOrdenTrabajo.Where(f => f.EstadoOrdenNavigation.Codigo.Equals("EXIT")).Count();
                dashBoardDTO.Progres = await GenerateProgresCard(listOrdenTrabajo.Count(), listOrdenTrabajo.Where(f => f.EstadoOrdenNavigation.Codigo.Equals("EXIT")).Count());
                dashBoardDTOs.Add(dashBoardDTO);

                /*segunda card de total de ordenes Razonadas*/
                dashBoardDTO = new DashBoardDTO();
                dashBoardDTO.Items = listOrdenTrabajo.Where(f => f.EstadoOrdenNavigation.Codigo.Equals("RAZO")).Count();
                dashBoardDTO.Progres = await GenerateProgresCard(listOrdenTrabajo.Count(), listOrdenTrabajo.Where(f => f.EstadoOrdenNavigation.Codigo.Equals("RAZO")).Count());
                dashBoardDTOs.Add(dashBoardDTO);



                return RequestResult<List<DashBoardDTO>>.CreateSuccessful(dashBoardDTOs);


            }
            catch (Exception ex)
            {
                return RequestResult<List<DashBoardDTO>>.CreateError(ex.Message);
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
            return  (itemsAvance * 100) / items;
        }
        #endregion
    }
}

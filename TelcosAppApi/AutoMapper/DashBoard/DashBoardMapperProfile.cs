using AplicationServices.DTOs.Generics;
using AutoMapper;
using TelcosAppApi.DataAccess.Entities;

namespace TelcosAppApi.AutoMapper.DashBoard
{
    public class DashBoardMapperProfile:Profile
    {
        public DashBoardMapperProfile()
        {
            FromOrdentrabajoToDashBoardDto();
        }
        /// <summary>
        /// Convierte desde rol hasta GenericDto
        /// </summary>
        private void FromOrdentrabajoToDashBoardDto()
        {
            CreateMap<OrdenTrabajo, DashBoardDTO>();
        }
    }
}

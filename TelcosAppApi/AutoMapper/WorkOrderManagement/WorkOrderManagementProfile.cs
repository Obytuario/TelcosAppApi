using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AplicationServices.DTOs.workOrderManagement;
using TelcosAppApi.DataAccess.Entities;

namespace TelcosAppApi.AutoMapper.WorkOrderManagement
{
    public class WorkOrderManagementProfile:Profile
    {
        private const string DATE_FORMAT = "dd/MM/yyyy";
        public WorkOrderManagementProfile()
        {
            FromPostWorkOrderDtoToOrdenTrabajo();
            FromOrdenTrabajoToGetWorkOrderManagementDTO();
        }
        /// <summary>
        /// Convierte desde Usuario hasta CredencialesUsuarioDto
        /// </summary>
        private void FromOrdenTrabajoToGetWorkOrderManagementDTO()
        {
            CreateMap<OrdenTrabajo, GetWorkOrderManagementDTO>()
                .ForMember(target => target.IdDto, opt => opt.MapFrom(source => source.ID))
                .ForMember(target => target.IdSuscriptorDto, opt => opt.MapFrom(source => source.Suscriptor))
                .ForMember(target => target.NumeroOrdenDto, opt => opt.MapFrom(source => source.NumeroOrden))
                .ForMember(target => target.NombreSuscriptorDto, opt => opt.MapFrom(source => source.SuscriptorNavigation.Nombre+" "+source.SuscriptorNavigation.Apellido??""))
                .ForMember(target => target.EstadoOrdenDTO, opt => opt.MapFrom(source => source.EstadoOrden))
                .ForMember(target => target.CuentaSuscriptorDto, opt => opt.MapFrom(source => source.SuscriptorNavigation.NumeroCuenta))
                .ForMember(target => target.NombreEstadoOrdenDTO, opt => opt.MapFrom(source => source.EstadoOrdenNavigation.Descripcion)); 
        }

        /// <summary>
        /// Convierte desde PostWorkOrderDto hasta orden de trabajo
        /// </summary>
        private void FromPostWorkOrderDtoToOrdenTrabajo()
        {
            _ = CreateMap<PostWorkOrderManagementDTO, OrdenTrabajo>()
                 .ForMember(target => target.ID, opt => opt.MapFrom(source => Guid.NewGuid()))
                 .ForMember(target => target.NumeroOrden, opt => opt.MapFrom(source => source.NumeroOrdenDto))
                 .ForMember(target => target.FechaOrden, opt => opt.MapFrom(source => DateTime.Now.Date))
                 .ForMember(target => target.FechaRegistro, opt => opt.MapFrom(source => DateTime.Now))
                 .ForMember(target => target.UsuarioRegistra, opt => opt.MapFrom(source => source.UsuarioRegistraDto))
                 .ForMember(target => target.EstadoOrden, opt => opt.MapFrom(source => source.EstadoOrdenDTO))
                 .ForMember(target => target.SuscriptorNavigation, opt => opt.MapFrom(source => new Suscriptor
                 {
                     ID = Guid.NewGuid(),
                     Nombre = source.suscriptorDTO.NombreDTO ?? "",
                     Apellido = source.suscriptorDTO.ApellidoDTO ?? "",
                     TipoSuscriptor = source.suscriptorDTO.TipoSuscriptorDto,
                     NumeroCuenta = source.suscriptorDTO.NumeroCuentaDto                    
                 }));
        }
    }
}

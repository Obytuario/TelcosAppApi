using AplicationServices.DTOs.WorkOrderFollowUp;
using AplicationServices.DTOs.workOrderManagement;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelcosAppApi.DataAccess.Entities;

namespace TelcosAppApi.AutoMapper.WorkOrderFollowUp
{
    public class WorkOrderFollowUpProfile: Profile
    {
        private const string DATE_FORMAT = "dd/MM/yyyy";
        public WorkOrderFollowUpProfile()
        {
            //FromPostWorkOrderDtoToOrdenTrabajo();
            FromOrdenTrabajoToGetWorkOrderFollowDTO();
            FromDetalleEquipoToDetailWorkOrderFollowequipment();
            FromDetalleMaterialToDetailWorkOrderFollowMaterial();
            FromDetailWorkOrderFollowequipmentToDetalleEquipo();
            FromDetailWorkOrderFollowequipmentToDetalleMaterial();
            FromOrdenTrabajoToGetWorkOrderBillingDTO();
        }
        /// <summary>
        /// Convierte desde orden trabajo a GetWorkOrderManagementDTO
        /// </summary>
        private void FromOrdenTrabajoToGetWorkOrderFollowDTO()
        {
            CreateMap<OrdenTrabajo, GetWorkOrderFollowUpDTO>()
                .ForMember(target => target.IdOrden, opt => opt.MapFrom(source => source.ID))
                .ForMember(target => target.NumeroDocumento, opt => opt.MapFrom(source => source.UsuarioRegistraNavigation.NumeroDocumento))
                .ForMember(target => target.NumeroOrden, opt => opt.MapFrom(source => source.NumeroOrden))
                .ForMember(target => target.EstadoOrden, opt => opt.MapFrom(source => source.EstadoOrdenNavigation.Descripcion))
                .ForMember(target => target.CodigoEstadoOrden, opt => opt.MapFrom(source => source.EstadoOrdenNavigation.Codigo))
                .ForMember(target => target.NombreTecnico, opt => opt.MapFrom(source => source.UsuarioRegistraNavigation.PrimerNombre + " " + source.UsuarioRegistraNavigation.Apellidos ?? ""))
                .ForMember(target => target.FechaOrdenTrabajo, opt => opt.MapFrom(source => source.FechaOrden))
                .ForMember(target => target.IdCarpeta, opt => opt.MapFrom(source => source.Carpeta))
                .ForMember(target => target.NombreCarpeta, opt => opt.MapFrom(source => source.CarpetaNavigation.Descripcion))
                .ForMember(target => target.DetalleEquipo, opt => opt.MapFrom(source => source.DetalleEquipoOrdenTrabajo.Select(s => new DetailWorkOrderFollowequipment()
                {
                    NombreEquipo = s.ParamEquipoActividadNavigation.EquipoNavigation.Descripcion,
                    SerialEquipo = s.Serial,
                    CodigoEquipo = s.ParamEquipoActividadNavigation.EquipoNavigation.Codigo,
                    NombreMovimiento = s.MovimientoEquipoNavigation.Descripcion
                })))
                .ForMember(target => target.Detallematerial, opt => opt.MapFrom(source => source.DetalleMaterialOrdenTrabajo.Select(s => new DetailWorkOrderFollowMaterial()
                {
                    NombreMaterial = s.ParamMaterialActividadNavigation.MaterialNavigation.Descripcion,
                    CantidadMaterial = s.Cantidad,
                    CodigoMaterial = s.ParamMaterialActividadNavigation.MaterialNavigation.Codigo                  
                })));
        }
        /// <summary>
        /// Convierte desde orden trabajo a GetWorkOrderBillingDTO
        /// </summary>
        private void FromOrdenTrabajoToGetWorkOrderBillingDTO()
        {
            CreateMap<OrdenTrabajo, GetWorkOrderBillingDTO>()
                .ForMember(target => target.IdOrden, opt => opt.MapFrom(source => source.ID))
                .ForMember(target => target.NumeroDocumento, opt => opt.MapFrom(source => source.UsuarioRegistraNavigation.NumeroDocumento))
                .ForMember(target => target.NumeroDocumentoAuxiliar, opt => opt.MapFrom(source => source.TecnicoAuxiliar == null ? "" : source.TecnicoAuxiliarNavigation.NumeroDocumento??""))
                .ForMember(target => target.NumeroOrden, opt => opt.MapFrom(source => source.NumeroOrden))
                .ForMember(target => target.NumeroCuenta, opt => opt.MapFrom(source => source.SuscriptorNavigation.NumeroCuenta))
                .ForMember(target => target.NombreAuxiliar, opt => opt.MapFrom(source => source.TecnicoAuxiliar == null?"": source.TecnicoAuxiliarNavigation.PrimerNombre??"" + " " + source.TecnicoAuxiliarNavigation.Apellidos ?? ""))
                .ForMember(target => target.NombreTecnico, opt => opt.MapFrom(source => source.UsuarioRegistraNavigation.PrimerNombre + " " + source.UsuarioRegistraNavigation.Apellidos ?? ""))
                .ForMember(target => target.FechaOrdenTrabajo, opt => opt.MapFrom(source => source.FechaOrden))
                .ForMember(target => target.CentroOperaciones, opt => opt.MapFrom(source => source.UsuarioRegistraNavigation.CentroOperacionNavigation.Descripcion));
                
               
        }
        /// <summary>
        /// Convierte desde detalle equipo a GetWorkOrderManagementDTO
        /// </summary>
        private void FromDetalleEquipoToDetailWorkOrderFollowequipment()
        {
            CreateMap<DetalleEquipoOrdenTrabajo, DetailWorkOrderFollowequipment>()
                .ForMember(target => target.IdDetalle, opt => opt.MapFrom(source => source.ID))
                .ForMember(target => target.IdParamActividad, opt => opt.MapFrom(source => source.ParamEquipoActividad))
                .ForMember(target => target.CodigoEquipo, opt => opt.MapFrom(source => source.ParamEquipoActividadNavigation.EquipoNavigation.Codigo))
                .ForMember(target => target.NombreEquipo, opt => opt.MapFrom(source => source.ParamEquipoActividadNavigation.EquipoNavigation.Descripcion))
                .ForMember(target => target.SerialEquipo, opt => opt.MapFrom(source => source.Serial))
                .ForMember(target => target.IdMovimiento, opt => opt.MapFrom(source => source.MovimientoEquipo))
                .ForMember(target => target.IdCarpeta, opt => opt.MapFrom(source => source.ParamEquipoActividadNavigation.ActividadNavigation.Carpeta))
                .ForMember(target => target.NombreMovimiento, opt => opt.MapFrom(source => source.MovimientoEquipoNavigation.Descripcion))
                .ForMember(target => target.IdActividad, opt => opt.MapFrom(source => source.ParamEquipoActividadNavigation.Actividad))
                .ForMember(target => target.NombreActividad, opt => opt.MapFrom(source => source.ParamEquipoActividadNavigation.ActividadNavigation.Descripcion));
        }
        /// <summary>
        /// Convierte desde orden trabajo a GetWorkOrderManagementDTO
        /// </summary>
        private void FromDetalleMaterialToDetailWorkOrderFollowMaterial()
        {
            CreateMap<DetalleMaterialOrdenTrabajo, DetailWorkOrderFollowMaterial>()
                .ForMember(target => target.IdDetalle, opt => opt.MapFrom(source => source.ID))
                .ForMember(target => target.IdParamActividad, opt => opt.MapFrom(source => source.ParamMaterialActividad))
                .ForMember(target => target.IdCarpeta, opt => opt.MapFrom(source => source.ParamMaterialActividadNavigation.ActividadNavigation.Carpeta))
                .ForMember(target => target.CantidadMaterial, opt => opt.MapFrom(source => source.Cantidad))
                .ForMember(target => target.CodigoMaterial, opt => opt.MapFrom(source => source.ParamMaterialActividadNavigation.MaterialNavigation.Codigo))
                .ForMember(target => target.NombreMaterial, opt => opt.MapFrom(source => source.ParamMaterialActividadNavigation.MaterialNavigation.Descripcion))
                .ForMember(target => target.IdActividad, opt => opt.MapFrom(source => source.ParamMaterialActividadNavigation.Actividad))
                .ForMember(target => target.NombreActividad, opt => opt.MapFrom(source => source.ParamMaterialActividadNavigation.ActividadNavigation.Descripcion));
           
        }

        private void FromDetailWorkOrderFollowequipmentToDetalleEquipo()
        {
            CreateMap<DetailWorkOrderFollowequipment, DetalleEquipoOrdenTrabajo>()
                .ForMember(target => target.ID, opt => opt.MapFrom(source => source.IdDetalle))
                .ForMember(target => target.ParamEquipoActividad, opt => opt.MapFrom(source => source.IdParamActividad))
                .ForMember(target => target.Activo, opt => opt.MapFrom(source => true))
                .ForMember(target => target.Serial, opt => opt.MapFrom(source => source.SerialEquipo))
                .ForMember(target => target.MovimientoEquipo, opt => opt.MapFrom(source => source.IdMovimiento));                
        }
        private void FromDetailWorkOrderFollowequipmentToDetalleMaterial()
        {
            CreateMap<DetailWorkOrderFollowMaterial, DetalleMaterialOrdenTrabajo>()
                .ForMember(target => target.ID, opt => opt.MapFrom(source => source.IdDetalle))
                .ForMember(target => target.ParamMaterialActividad, opt => opt.MapFrom(source => source.IdParamActividad))
                .ForMember(target => target.Activo, opt => opt.MapFrom(source => true))
                .ForMember(target => target.Cantidad, opt => opt.MapFrom(source => source.CantidadMaterial));
                
        }

        /// <summary>
        /// Convierte desde PostWorkOrderDto hasta orden de trabajo
        /// </summary>
        //private void FromPostWorkOrderDtoToOrdenTrabajo()
        //{
        //    _ = CreateMap<PostWorkOrderManagementDTO, OrdenTrabajo>()
        //         .ForMember(target => target.ID, opt => opt.MapFrom(source => Guid.NewGuid()))
        //         .ForMember(target => target.NumeroOrden, opt => opt.MapFrom(source => source.NumeroOrdenDto))
        //         .ForMember(target => target.UsuarioRegistra, opt => opt.MapFrom(source => source.UsuarioRegistraDto))
        //         .ForMember(target => target.EstadoOrden, opt => opt.MapFrom(source => source.EstadoOrdenDTO))
        //         .ForMember(target => target.SuscriptorNavigation, opt => opt.MapFrom(source => new Suscriptor
        //         {
        //             ID = Guid.NewGuid(),
        //             Nombre = source.suscriptorDTO.NombreDTO ?? "",
        //             Apellido = source.suscriptorDTO.ApellidoDTO ?? "",
        //             TipoSuscriptor = source.suscriptorDTO.TipoSuscriptorDto,
        //             NumeroCuenta = source.suscriptorDTO.NumeroCuentaDto
        //         }));
        //}
    }
}

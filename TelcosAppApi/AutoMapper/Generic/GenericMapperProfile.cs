using AutoMapper;
using AplicationServices.DTOs.Generics;
using TelcosAppApi.DataAccess.Entities;

namespace TelcosAppApi.AutoMapper.Generic
{
    public class GenericMapperProfile:Profile
    {
        public GenericMapperProfile()
        {
            FromRolToGenericDto();
            FromTipoSuscriptorToGenericDto();
            FromEstadoOrdenTrabajoToGenericDto();
            FromCargoToGenericDto();
            FromCentrooperacionToGenericDto();
            FromCarpetaToGenericDto();
            FromParamEquipoToGenericDto();
            FromParamMaterialToGenericDto();
            FromMovimientoToGenericDto();
        }
        /// <summary>
        /// Convierte desde rol hasta GenericDto
        /// </summary>
        private void FromRolToGenericDto()
        {
            CreateMap<Rol, GenericDto>()
              .ForMember(target => target.IdDto, opt => opt.MapFrom(source => source.ID))
              .ForMember(target => target.CodigoDto, opt => opt.MapFrom(source => source.Codigo))
              .ForMember(target => target.DescripcionDto, opt => opt.MapFrom(source => source.Descripcion)); 
        }
        /// <summary>
        /// Convierte desde tipo suscriptor hasta GenericDto
        /// </summary>
        private void FromTipoSuscriptorToGenericDto()
        {
            CreateMap<TipoSuscriptor, GenericDto>()
              .ForMember(target => target.IdDto, opt => opt.MapFrom(source => source.ID))
              .ForMember(target => target.CodigoDto, opt => opt.MapFrom(source => source.Codigo))
              .ForMember(target => target.DescripcionDto, opt => opt.MapFrom(source => source.Descripcion));
        }
        /// <summary>
        /// Convierte desde tipo suscriptor hasta GenericDto
        /// </summary>
        private void FromEstadoOrdenTrabajoToGenericDto()
        {
            CreateMap<EstadoOrdenTrabajo, GenericDto>()
              .ForMember(target => target.IdDto, opt => opt.MapFrom(source => source.ID))
              .ForMember(target => target.CodigoDto, opt => opt.MapFrom(source => source.Codigo))
              .ForMember(target => target.DescripcionDto, opt => opt.MapFrom(source => source.Descripcion));
        }
        /// <summary>
        /// Convierte desde cargo hasta GenericDto
        /// </summary>
        private void FromCargoToGenericDto()
        {
            CreateMap<Cargo, GenericDto>()
              .ForMember(target => target.IdDto, opt => opt.MapFrom(source => source.ID))
              .ForMember(target => target.CodigoDto, opt => opt.MapFrom(source => source.Codigo))
              .ForMember(target => target.DescripcionDto, opt => opt.MapFrom(source => source.Descripcion));
        }
        /// <summary>
        /// Convierte desde centro operacion hasta GenericDto
        /// </summary>
        private void FromCentrooperacionToGenericDto()
        {
            CreateMap<CentroOperacion, GenericDto>()
              .ForMember(target => target.IdDto, opt => opt.MapFrom(source => source.ID))
              .ForMember(target => target.CodigoDto, opt => opt.MapFrom(source => source.Codigo))
              .ForMember(target => target.DescripcionDto, opt => opt.MapFrom(source => source.Descripcion));
        }
        /// <summary>
        /// Convierte carpeta hasta GenericDto
        /// </summary>
        private void FromCarpetaToGenericDto()
        {
            CreateMap<Carpeta, GenericDto>()
              .ForMember(target => target.IdDto, opt => opt.MapFrom(source => source.ID))
              .ForMember(target => target.CodigoDto, opt => opt.MapFrom(source => source.Codigo))
              .ForMember(target => target.DescripcionDto, opt => opt.MapFrom(source => source.Descripcion));
        }
        /// <summary>
        /// Convierte movimiento hasta GenericDto
        /// </summary>
        private void FromMovimientoToGenericDto()
        {
            CreateMap<MovimientoEquipo, GenericDto>()
              .ForMember(target => target.IdDto, opt => opt.MapFrom(source => source.ID))
              .ForMember(target => target.CodigoDto, opt => opt.MapFrom(source => source.Codigo))
              .ForMember(target => target.DescripcionDto, opt => opt.MapFrom(source => source.Descripcion));
        }
        /// <summary>
        /// Convierte carpeta hasta GenericDto
        /// </summary>
        private void FromParamEquipoToGenericDto()
        {
            CreateMap<ParamEquipoActividad, paramGenericDto>()
              .ForMember(target => target.IdParamGenericActividad, opt => opt.MapFrom(source => source.ID))
              .ForMember(target => target.NombreActividad, opt => opt.MapFrom(source => source.ActividadNavigation.Descripcion))
              .ForMember(target => target.NombreGeneric, opt => opt.MapFrom(source => source.EquipoNavigation.Descripcion));
        }
        /// <summary>
        /// Convierte carpeta hasta GenericDto
        /// </summary>
        private void FromParamMaterialToGenericDto()
        {
            CreateMap<ParamMaterialActividad, paramGenericDto>()
              .ForMember(target => target.IdParamGenericActividad, opt => opt.MapFrom(source => source.ID))
              .ForMember(target => target.NombreActividad, opt => opt.MapFrom(source => source.ActividadNavigation))
              .ForMember(target => target.NombreGeneric, opt => opt.MapFrom(source => source.MaterialNavigation.Descripcion));
        }
    }
}

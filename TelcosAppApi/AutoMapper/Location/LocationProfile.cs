using AplicationServices.DTOs.Location;
using AutoMapper;
using TelcosAppApi.AplicationServices.DTOs.Authentication;
using TelcosAppApi.DataAccess.Entities;

namespace TelcosAppApi.AutoMapper.Location
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            FromLocationDtoToUbicacionUsuario();
            FromCredencialesDtoToLocationDto();
            FromUbicacionUsuarioToLocationDto();
            FromOrdenTrabajoToGetLocationWorkOrderDto();
        }
        /// <summary>
        /// Convierte desde CredencialesUsuarioDto hasta Usuario
        /// </summary>
        private void FromLocationDtoToUbicacionUsuario()
        {
            CreateMap<LocationDto, UbicacionUsuario>()
                .ForMember(target => target.ID, opt => opt.MapFrom(source => Guid.NewGuid()))
                .ForMember(target => target.Latitud, opt => opt.MapFrom(source => Convert.ToString(source.Latitude) ))
                .ForMember(target => target.Longitud, opt => opt.MapFrom(source => Convert.ToString(source.Longitude)))
                .ForMember(target => target.FechaHora, opt => opt.MapFrom(source => DateTime.Now))
                .ForMember(target => target.Usuario, opt => opt.MapFrom(source => source.IdUser));
            
        }
        /// <summary>
        /// Convierte desde CredencialesUsuarioDto hasta Usuario
        /// </summary>
        private void FromUbicacionUsuarioToLocationDto()
        {
            CreateMap<UbicacionUsuario, GetLocationUserDto>()
                .ForMember(target => target.IdUser, opt => opt.MapFrom(source => source.ID))
                .ForMember(target => target.Latitude, opt => opt.MapFrom(source => Convert.ToDecimal(source.Latitud)))
                .ForMember(target => target.Longitude, opt => opt.MapFrom(source => Convert.ToDecimal(source.Longitud)))
                .ForMember(target => target.labelOptions, opt => opt.MapFrom(source => new GetLabelUserDto
                {
                    Text = source.UsuarioNavigation.PrimerNombre + " " + source.UsuarioNavigation.Apellidos ?? ""
                    
                })); 
          
        }
        /// <summary>
        /// Convierte desde orden de trabajo a GetLocationWorkOrderDto
        /// </summary>
        private void FromOrdenTrabajoToGetLocationWorkOrderDto()
        {
            CreateMap<OrdenTrabajo, GetLocationWorkOrderDto>()
                .ForMember(target => target.IdWork, opt => opt.MapFrom(source => source.ID))
                .ForMember(target => target.Latitude, opt => opt.MapFrom(source => Convert.ToDecimal(source.Latitud)))
                .ForMember(target => target.Longitude, opt => opt.MapFrom(source => Convert.ToDecimal(source.Logitud)))
                .ForMember(target => target.Tecnico, opt => opt.MapFrom(source => source.UsuarioRegistraNavigation.PrimerNombre + " " + source.UsuarioRegistraNavigation.Apellidos ?? ""))
                .ForMember(target => target.Suscriptor, opt => opt.MapFrom(source => source.SuscriptorNavigation.Nombre + " " + source.SuscriptorNavigation.Apellido ?? ""))
                .ForMember(target => target.labelOptions, opt => opt.MapFrom(source => new GetLabelWorkOrderDto
                {
                    Text = "OT = " +source.NumeroOrden

                }));

        }
        
        /// <summary>
        /// Convierte desde CredencialesUsuarioDto hasta Usuario
        /// </summary>
        private void FromCredencialesDtoToLocationDto()
        {
            CreateMap<CredencialesUsuarioDto, LocationDto>()
                .ForMember(target => target.Latitude, opt => opt.MapFrom(source => source.Latitude??0))
                .ForMember(target => target.Longitude, opt => opt.MapFrom(source => source.Longitude??0));
                //.ForMember(target => target.FechaHora, opt => opt.MapFrom(source => DateTime.Now))
                //.ForMember(target => target.Usuario, opt => opt.MapFrom(source => source.User));

        }
    }
}

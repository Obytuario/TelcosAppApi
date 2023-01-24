using AutoMapper;
using TelcosAppApi.AplicationServices.DTOs.Generics;
using TelcosAppApi.DataAccess.Entities;

namespace TelcosAppApi.AutoMapper.Generic
{
    public class GenericMapperProfile:Profile
    {
        public GenericMapperProfile()
        {
            FromRolToGenericDto();
            // FromUsuarioToCredencialesUsuarioDto();
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
    }
}

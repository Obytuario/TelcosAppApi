
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TelcosAppApi.AplicationServices.DTOs.Authentication;
using TelcosAppApi.DataAccess.Entities;

namespace TelcosAppApi.AutoMapper.Authentication
{
    
    public class AuthenticationMapperProfile: Profile
    {
        private const string DATE_FORMAT = "dd/MM/yyyy";
        public AuthenticationMapperProfile()
        {
            FromCredencialesUsuarioDtoToUsuario();
           // FromUsuarioToCredencialesUsuarioDto();
        }
        /// <summary>
        /// Convierte desde Usuario hasta CredencialesUsuarioDto
        /// </summary>
        private void FromUsuarioToCredencialesUsuarioDto()
        {
            CreateMap<Usuario, CredencialesUsuarioDto>()
                .ForMember(target => target.User, opt => opt.MapFrom(source => source.NumeroDocumento));;
        }

        /// <summary>
        /// Convierte desde CredencialesUsuarioDto hasta Usuario
        /// </summary>
        private void FromCredencialesUsuarioDtoToUsuario()
        {
            CreateMap<CredencialesUsuarioDto, Usuario>()
                .ForMember(target => target.NumeroDocumento, opt => opt.MapFrom(source => source.User))
                .ForMember(target => target.Contraseña, opt => opt.MapFrom(source => source.Password));
;
        }
        
    }
}

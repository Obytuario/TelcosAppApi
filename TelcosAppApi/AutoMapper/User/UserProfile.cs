using AplicationServices.DTOs.User;
using AplicationServices.DTOs.workOrderManagement;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using TelcosAppApi.DataAccess.Entities;

namespace TelcosAppApi.AutoMapper.User
{
    public class UserProfile:Profile
    {
        private const string DATE_FORMAT = "dd/MM/yyyy";
        public UserProfile()
        {
            FromUserDtoToUsuario();
           
        }
       
        private void FromUserDtoToUsuario()
        {
            _ = CreateMap<PostUserDto, Usuario>()
                 .ForMember(target => target.ID, opt => opt.MapFrom(source => source.id != null ? source.id: Guid.NewGuid()))
                 .ForMember(target => target.Activo, opt => opt.MapFrom(source => true))
                 .ForMember(target => target.GenerarContraseña, opt => opt.MapFrom(source => true))
                 .ForMember(target => target.NumeroDocumento, opt => opt.MapFrom(source => source.numberDocument))
                 .ForMember(target => target.Rol, opt => opt.MapFrom(source => source.idRol))
                 .ForMember(target => target.PrimerNombre, opt => opt.MapFrom(source => source.fName))
                 .ForMember(target => target.Apellidos, opt => opt.MapFrom(source => source.lName));
        }
    }
}

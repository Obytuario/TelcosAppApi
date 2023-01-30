using AplicationServices.DTOs.User;
using AplicationServices.DTOs.workOrderManagement;
using AutoMapper;
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
                 .ForMember(target => target.ID, opt => opt.MapFrom(source => Guid.NewGuid()))
                 .ForMember(target => target.NumeroDocumento, opt => opt.MapFrom(source => source.NumeroDocumentoDto))
                 .ForMember(target => target.Rol, opt => opt.MapFrom(source => source.RolDto))
                 .ForMember(target => target.PrimerNombre, opt => opt.MapFrom(source => source.PrimerNombreDto));
        }
    }
}

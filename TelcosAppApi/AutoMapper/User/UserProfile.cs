using AplicationServices.DTOs.User;
using AplicationServices.DTOs.workOrderManagement;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System;
using TelcosAppApi.DataAccess.Entities;

namespace TelcosAppApi.AutoMapper.User
{
    public class UserProfile:Profile
    {
        private const string DATE_FORMAT = "dd/MM/yyyy";
        public UserProfile()
        {
            FromUserDtoToUsuario();
            FromUserUsuarioToDto();


        }
       
        private void FromUserDtoToUsuario()
        {
            _ = CreateMap<PostUserDto, Usuario>()
                 .ForMember(target => target.ID, opt => opt.MapFrom(source => source.id != null ? source.id: Guid.NewGuid()))
                 .ForMember(target => target.Activo, opt => opt.MapFrom(source => source.active))
                 .ForMember(target => target.GenerarContraseña, opt => opt.MapFrom(source => true))
                 .ForMember(target => target.NumeroDocumento, opt => opt.MapFrom(source => source.numberDocument))
                 .ForMember(target => target.Rol, opt => opt.MapFrom(source => source.idRol))
                 .ForMember(target => target.Cargo, opt => opt.MapFrom(source => source.idCharge))
                 .ForMember(target => target.CentroOperacion, opt => opt.MapFrom(source => source.idOperationCenter))
                 .ForMember(target => target.PrimerNombre, opt => opt.MapFrom(source => source.fName))
                 .ForMember(target => target.NumeroContacto, opt => opt.MapFrom(source => source.mobile))
                 .ForMember(target => target.Correo, opt => opt.MapFrom(source => source.email))
                 .ForMember(target => target.Apellidos, opt => opt.MapFrom(source => source.lName));
        }
        private void FromUserUsuarioToDto()
        {
            _ = CreateMap<Usuario, PostUserDto>()
                 .ForMember(target => target.id, opt => opt.MapFrom(source => source.ID))
                 .ForMember(target => target.active, opt => opt.MapFrom(source => source.Activo))
                 //.ForMember(target => target.GenerarContraseña, opt => opt.MapFrom(source => true))
                 .ForMember(target => target.numberDocument, opt => opt.MapFrom(source => source.NumeroDocumento))
                 .ForMember(target => target.idRol, opt => opt.MapFrom(source => source.Rol))
                 .ForMember(target => target.rol, opt => opt.MapFrom(source => source.RolNavigation.Descripcion))
                 .ForMember(target => target.idCharge, opt => opt.MapFrom(source => source.Cargo))
                 .ForMember(target => target.charge, opt => opt.MapFrom(source => source.CargoNavigation.Descripcion))
                 .ForMember(target => target.active, opt => opt.MapFrom(source => source.Activo))
                 .ForMember(target => target.email, opt => opt.MapFrom(source => source.RolNavigation.Descripcion))
                 .ForMember(target => target.mobile, opt => opt.MapFrom(source => source.NumeroDocumento))
                 .ForMember(target => target.idOperationCenter, opt => opt.MapFrom(source => source.CentroOperacion))
                 .ForMember(target => target.operationCenter, opt => opt.MapFrom(source => source.CentroOperacionNavigation.Descripcion))
                 .ForMember(target => target.lName, opt => opt.MapFrom(source => source.Apellidos))
                 .ForMember(target => target.email, opt => opt.MapFrom(source => source.Correo))
                 .ForMember(target => target.mobile, opt => opt.MapFrom(source => source.NumeroContacto))
                 .ForMember(target => target.fName, opt => opt.MapFrom(source => source.PrimerNombre));
                 //.ForMember(target => target.fName, opt => opt.MapFrom(source => source.Apellidos));
        }
    }
}

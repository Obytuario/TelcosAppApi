using AplicationServices.DTOs.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelcosAppApi.AplicationServices.DTOs.Authentication;

namespace TelcosAppApi.AplicationServices.Application.Contracts.Authentication
{
    public interface IAuthenticationServices
    {
        Task<RequestResult<RespuestaAutenticacionDto>> Login(CredencialesUsuarioDto credencialesUsuario);
        Task<RequestResult<RespuestaAutenticacionDto>> RestoreLogin(CredencialesUsuarioDto credencialesUsuario);
        Task<RequestResult<string>> CargaMasiva(CargaDto cargaDto);
        Task<RequestResult<List<CargaUserDto>>> CargaMasivaUsuarios(List<CargaUserDto> userDtos);
    }
}

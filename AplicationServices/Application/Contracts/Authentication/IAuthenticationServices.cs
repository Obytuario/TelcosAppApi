using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelcosAppApi.AplicationServices.DTOs.Authentication;

namespace AplicationServices.Application.Contracts.Authentication
{
    public interface IAuthenticationServices
    {
        Task<RespuestaAutenticacionDto?> Login(CredencialesUsuarioDto credencialesUsuario);
    }
}

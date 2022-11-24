using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelcosAppApi.DataAccess.Entities;

namespace DomainServices.Domain.Contracts.Authentication
{
    public interface IAuthenticationDomain
    {
        Task<Usuario?> Login(Usuario credencialesUsuario);
    }
}

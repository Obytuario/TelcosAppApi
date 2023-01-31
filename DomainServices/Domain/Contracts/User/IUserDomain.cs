using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelcosAppApi.DataAccess.Entities;

namespace DomainServices.Domain.Contracts.User
{
    public interface IUserDomain
    {
        Task<Usuario> GetUser(string numeroDocumento);
        void SaveUser(Usuario user);
    }
}

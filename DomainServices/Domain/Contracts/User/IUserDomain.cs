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
        Task<List<Usuario>> GetAllUser();
        Task<List<Usuario>> GetUserAssignmentById(Guid user);
        Task<List<Usuario>> GetUserAssignmentByRol(Guid rol);
        Task<Usuario> GetUser(string numeroDocumento);
        Task<Usuario> GetUserById(Guid user);
        void SaveUser(Usuario user);
        void UpdateUser(Usuario userUpdate, Usuario user);
    }
}

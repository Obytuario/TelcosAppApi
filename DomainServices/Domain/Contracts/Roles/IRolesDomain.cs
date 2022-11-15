using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelcosAppApi.DataAccess.Entities;

namespace TelcosAppApi.DomainServices.Domain.Contracts.Roles
{
    public interface IRolesDomain
    {
        Task<List<Rol>> GetRoles();
    }
}

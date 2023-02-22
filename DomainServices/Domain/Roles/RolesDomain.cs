using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelcosAppApi.DataAccess.DataAccess;
using TelcosAppApi.DataAccess.Entities;
using DomainServices.Domain.Contracts.Roles;

namespace DomainServices.Domain.Roles
{
    public class RolesDomain:IRolesDomain
    {
        private readonly TelcosSuiteContext _context;

        public RolesDomain(TelcosSuiteContext telcosApiContext)
        {
            _context = telcosApiContext;
        }
        #region Method
        public async Task<List<Rol>> GetRoles()
        {
            return await _context.Rol.ToListAsync();         
        }
        #endregion|

    }
}

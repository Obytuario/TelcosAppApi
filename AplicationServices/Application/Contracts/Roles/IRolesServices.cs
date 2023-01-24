using AplicationServices.DTOs.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelcosAppApi.AplicationServices.DTOs.Generics;
using TelcosAppApi.DataAccess.Entities;

namespace TelcosAppApi.AplicationServices.Application.Contracts.Roles
{
    public interface IRolesServices
    {
        Task<RequestResult<List<GenericDto>>> GetRoles();
    }
}

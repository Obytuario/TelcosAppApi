using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelcosAppApi.DataAccess.Entities;

namespace DomainServices.Domain.Contracts.DashBoard
{
    public interface IDashBoardDomain
    {
        Task<List<OrdenTrabajo>> GetDashBoard();
        Task<List<Usuario>> GetUserActiveLocation();
    }
}

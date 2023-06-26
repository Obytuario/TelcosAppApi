using DomainServices.Domain.Contracts.DashBoard;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelcosAppApi.DataAccess.Entities;

namespace DomainServices.Domain.DashBoard
{
    public class DashBoardDomain: IDashBoardDomain
    {
        private readonly TelcosSuiteContext _context;
        public DashBoardDomain(TelcosSuiteContext telcosApiContext)
        {
            _context = telcosApiContext;
        }
        #region Method
        public async Task<List<OrdenTrabajo>> GetDashBoard()
        {
            return await _context.OrdenTrabajo.Where(ot => ot.FechaRegistro.Date.Equals(DateTime.Now.Date)).Include(i => i.EstadoOrdenNavigation).ToListAsync();
        }
        #endregion|

    }
}

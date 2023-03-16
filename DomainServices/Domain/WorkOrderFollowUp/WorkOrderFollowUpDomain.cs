using DomainServices.Domain.Contracts.WorkOrderFollowUp;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelcosAppApi.DataAccess.Entities;

namespace DomainServices.Domain.WorkOrderFollowUp
{
    public class WorkOrderFollowUpDomain:IWorkOrderFollowUpDomain
    {
        private readonly TelcosSuiteContext _context;

        public WorkOrderFollowUpDomain(TelcosSuiteContext telcosApiContext)
        {
            _context = telcosApiContext;
        }
        #region Method
        public async Task<List<OrdenTrabajo>> GetWorkOrderFollowUp(DateTime fechaInicio, DateTime fechaFinal)
        {
            return await _context.OrdenTrabajo.Where(x => x.FechaRegistro >= fechaInicio && x.FechaRegistro <= fechaFinal)
                .Include(x => x.EstadoOrdenNavigation)
                .Include(x => x.SuscriptorNavigation)
                .ToListAsync();
        }
        
        #endregion|
    }
}

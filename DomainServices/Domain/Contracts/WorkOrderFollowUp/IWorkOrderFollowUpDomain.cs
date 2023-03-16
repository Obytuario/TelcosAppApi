using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelcosAppApi.DataAccess.Entities;

namespace DomainServices.Domain.Contracts.WorkOrderFollowUp
{
    public interface IWorkOrderFollowUpDomain
    {
        Task<List<OrdenTrabajo>> GetWorkOrderFollowUp(DateTime fechaInicio, DateTime fechaFinal);
    }
}

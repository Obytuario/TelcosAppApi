using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelcosAppApi.DataAccess.Entities;

namespace DomainServices.Domain.Contracts.WorkOrderManagement
{
    public interface IWorkOrderManagementDomain
    {
        Task<List<OrdenTrabajo>> GetWorkOrderByUser(Guid? user);
        Task<List<TipoSuscriptor>> GetSubscriberType();
        Task<List<EstadoOrdenTrabajo>> GetWorkOrderStatus();
        void SaveWorkOrder(OrdenTrabajo workOrder);
    }
}

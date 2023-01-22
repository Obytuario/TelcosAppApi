using AplicationServices.DTOs.workOrderManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelcosAppApi.DataAccess.Entities;

namespace AplicationServices.Application.Contracts.WorkOrderManagement
{
    public interface IWorkOrderManagementServices
    {
        Task<List<GetWorkOrderManagementDTO>> GetWorkOrderByUser(Guid? user);
        Task<PostWorkOrderManagementDTO> SaveWorkOrder(PostWorkOrderManagementDTO workOrder);
    }
}

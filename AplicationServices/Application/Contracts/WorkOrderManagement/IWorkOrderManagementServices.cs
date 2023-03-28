using AplicationServices.DTOs.Generics;
using AplicationServices.DTOs.workOrderManagement;
using Microsoft.AspNetCore.Mvc;
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
        Task<RequestResult<List<GetWorkOrderManagementDTO>>> GetWorkOrderByUser(Guid? user);
        Task<RequestResult<List<GenericDto>>> GetSubscriberType();
        Task<RequestResult<List<GenericDto>>> GetWorkOrderStatus();
        Task<RequestResult<PostWorkOrderManagementDTO>> SaveWorkOrder(PostWorkOrderManagementDTO workOrder);
        Task<RequestResult<Guid>> UpdateManageWorkOrder(UpdateWorkOrderManagementDTO workOrder);

    }
}

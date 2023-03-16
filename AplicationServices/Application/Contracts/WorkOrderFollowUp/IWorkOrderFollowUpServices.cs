using AplicationServices.DTOs.Generics;
using AplicationServices.DTOs.WorkOrderFollowUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationServices.Application.Contracts.WorkOrderFollowUp
{
    public interface IWorkOrderFollowUpServices
    {
        Task<RequestResult<List<GetWorkOrderFollowUpDTO>>> GetWorkOrderFollowUp(PostWorkOrderFollowUpDTO filter);
    }
}

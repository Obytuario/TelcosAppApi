using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationServices.DTOs.workOrderManagement
{
    public class CancelWorkOrderManagementDTO
    {
        public Guid IdWorkOrder { get; set; }

        public Guid IdReasonCancellation { get; set; }

        public Guid IdUser { get; set; }
       
    }
}

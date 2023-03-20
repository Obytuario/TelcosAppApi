using AplicationServices.DTOs.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationServices.Application.Contracts.Carpetas
{
    public interface ICarpetasServices
    {
        Task<RequestResult<List<GenericDto>>> GetFiles();
        Task<RequestResult<List<paramGenericDto>>> GetActyvitiMaterialByFile(Guid file);
        Task<RequestResult<List<paramGenericDto>>> GetActyvitiEquipmentByFile(Guid file);
    }
}

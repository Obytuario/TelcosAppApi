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
        Task<RequestResult<List<paramGenericDto>>> GetEquipmentByActivity(Guid activity);
        Task<RequestResult<List<paramGenericDto>>> GetMaterialByActivity(Guid activity);
        Task<RequestResult<List<imageGenericDto>>> GetImageById(Guid ordenTrabajo);
    }
}

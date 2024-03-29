﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelcosAppApi.DataAccess.Entities;

namespace DomainServices.Domain.Contracts.Carpetas
{
    public interface ICarpetaDomain
    {
        Task<List<Carpeta>> GetCarpetas();
        Task<List<ParamEquipoActividad>> GetActyvitiEquipmentByFile(Guid file);
        Task<List<ParamMaterialActividad>> GetActyvitiMaterialByFile(Guid file);
        Task<List<ParamEquipoActividad>> GetEquipmentByActivity(Guid activity);
        Task<List<ParamMaterialActividad>> GetMaterialByActivity(Guid activity);
        Task<List<DetalleImagenOrdenTrabajo>> GetImageById(Guid ordenTrabajo);
    }
}

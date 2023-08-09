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
        Task<List<DetalleEquipoOrdenTrabajo>> GetDetailEquipmentByOrder(Guid order);
        Task<List<DetalleMaterialOrdenTrabajo>> GetDetailMaterialByOrder(Guid order);
        Task<List<MovimientoEquipo>> GetAllMovimientoEquipment();
        void UpdateDetailMaterial(DetalleMaterialOrdenTrabajo detailUpdate);
        void UpdateDetailEquipment(DetalleEquipoOrdenTrabajo detailUpdate);
        Task<List<Actividad>> GetActivity(Guid? carpeta);
        Task<List<TipoImagen>> GetPhotoType();
        Task<List<OrdenTrabajo>> GetWorkOrderBilling(DateTime fechaInicio, DateTime fechaFinal);
        List<LogDetalleEquipoOrdenTrabajo> GetWorkOrderEquipmentLogByID(Guid idDetalle);
        List<LogDetalleMaterialOrdenTrabajo> GetWorkOrderMaterialLogByID(Guid idDetalle);
    }
}

using Microsoft.EntityFrameworkCore;
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
        Task<OrdenTrabajo> GetWorkOrderById(Guid Id);
        Task<OrdenTrabajo> GetWorkOrderByNumber(string number);
        void SaveDetalleEquipoOrdenTrabajo(ICollection<DetalleEquipoOrdenTrabajo> detalleEquipoOrdenTrabajo);
        void SaveDetalleMaterialOrdenTrabajo(ICollection<DetalleMaterialOrdenTrabajo> detalleMaterialOrdenTrabajo);
        void SaveDetalleImagenOrdenTrabajo(List<DetalleImagenOrdenTrabajo> detalleImagenOrdenTrabajo);
        void SaveChanges();

        void saveDetalleCancelacionOrden(DetalleCancelacionOrden detalleCancelacionOrden);
        Task<List<MotivoCancelacionOrden>> GetWorkOrderReasonCancel();
        Task<List<ParamEquipoActividad>> GetParamEquipmentByActivity(Guid activity);
        Task<List<ParamMaterialActividad>> GetParammaterialByActivity(Guid activity);
        Task<List<MovimientoEquipo>> GetMovimientoEquipo();


    }
}

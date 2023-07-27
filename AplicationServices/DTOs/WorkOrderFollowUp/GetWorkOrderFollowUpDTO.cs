using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationServices.DTOs.WorkOrderFollowUp
{
    public class GetWorkOrderFollowUpDTO
    {
        public Guid IdOrden { get; set; }
        public string? NumeroOrden { get; set; }
        public string? Nodo { get; set; }
        public string NombreTecnico { get; set; }        
        public string NumeroDocumento { get; set; }
        public string EstadoOrden { get; set; }
        public string CodigoEstadoOrden { get; set; }
        public DateTime FechaOrdenTrabajo { get; set; }
        public Guid IdCarpeta { get; set; }
        public string NombreCarpeta { get; set; }
        public List<DetailWorkOrderFollowMaterial> Detallematerial  { get; set; }
        public List<DetailWorkOrderFollowequipment> DetalleEquipo { get; set; }
        
        

    }
    public class DetailWorkOrderFollowMaterial
    {
        public Guid IdDetalle { get; set; }
        public Guid IdCarpeta { get; set; }
        public Guid IdParamActividad { get; set; }
        public Guid IdActividad { get; set; }
        public Guid IdUsuarioRegistra { get; set; }
        public string? ObservacionModifica { get; set; }
        public string? CodigoMaterial { get; set; }
        public string? NombreMaterial { get; set; }
        public string? UsuarioRegistra { get; set; }
        public int CantidadMaterial { get; set; }
        public string? NombreActividad { get; set; }
        public string? CodigoActividad { get; set; }        
        public List<LogReportMaterialDetailDTO>? ReporteMaterialDetalleHistorico { get; set; }

    }
    public class DetailWorkOrderFollowequipment
    {
        public Guid IdDetalle { get; set; }
        public string? CodigoEquipo { get; set; }
        public string? NombreEquipo { get; set; }
        public string? UsuarioRegistra { get; set; }
        public string? SerialEquipo { get; set; }
        public Guid IdMovimiento { get; set; }
        public Guid IdParamActividad { get; set; }
        public Guid IdCarpeta { get; set; }
        public Guid IdActividad { get; set; }
        public Guid IdUsuarioRegistra { get; set; }
        public string? ObservacionModifica { get; set; }
        public string? NombreMovimiento { get; set; }
        public string? NombreActividad { get; set; }
        public string? CodigoActividad { get; set; }
        public List<LogReportEquipmentDetailDTO>? ReporteEquipoDetalleHistorico { get; set; }


    }
    public class LogReportEquipmentDetailDTO
    {
        
        public string? NumeroOrden { get; set; }
        public string NombreTecnico { get; set; }
        public string NumeroDocumento { get; set; }
        public string EstadoOrden { get; set; }
        public string CodigoEstadoOrden { get; set; }
        public string NombreEquipo { get; set; }
        public string SerialEquipo { get; set; }
        public string MovimientoEquipo { get; set; }
        public string ObservacionModifica { get; set; }
        public string UsuarioRegistra { get; set; }
        public DateTime FechaModifica { get; set; }
        public DateTime FechaOrdenTrabajo { get; set; }        
        public string NombreCarpeta { get; set; }

    }
    public class LogReportMaterialDetailDTO
    {

        public string? NumeroOrden { get; set; }
        public string NombreTecnico { get; set; }
        public string NombreMaterial { get; set; }
        public int CantidadMaterial { get; set; }
        public string NumeroDocumento { get; set; }
        public string EstadoOrden { get; set; }
        public string CodigoEstadoOrden { get; set; }
        public string ObservacionModifica { get; set; }
        public string UsuarioRegistra { get; set; }
        public DateTime FechaModifica { get; set; }
        public DateTime FechaOrdenTrabajo { get; set; }
        public string NombreCarpeta { get; set; }

    }
    public class GetWorkOrderBillingDTO
    {
        public Guid IdOrden { get; set; }
        public string? NumeroOrden { get; set; }
        public string NumeroCuenta { get; set; }
        public string NombreTecnico { get; set; }
        public string NumeroDocumento { get; set; }
        public string NombreAuxiliar { get; set; }
        public string NumeroDocumentoAuxiliar { get; set; }
        public string CentroOperaciones { get; set; }
        public string CodigoActividad { get; set; }
        public DateTime FechaOrdenTrabajo { get; set; }        
        public string Puntaje { get; set; }
        

    }
}

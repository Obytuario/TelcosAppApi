using DomainServices.Domain.Contracts.WorkOrderFollowUp;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelcosAppApi.DataAccess.Entities;

namespace DomainServices.Domain.WorkOrderFollowUp
{
    public class WorkOrderFollowUpDomain:IWorkOrderFollowUpDomain
    {
        public readonly string CODIGO_ESTADO_EXITOSA = "EXIT";
        private readonly TelcosSuiteContext _context;

        public WorkOrderFollowUpDomain(TelcosSuiteContext telcosApiContext)
        {
            _context = telcosApiContext;
        }
        #region Method
        public async Task<List<OrdenTrabajo>> GetWorkOrderFollowUp(DateTime fechaInicio, DateTime fechaFinal)
        {
            return await _context.OrdenTrabajo.Where(x => x.FechaOrden.Date >= fechaInicio.Date && x.FechaOrden.Date <= fechaFinal.Date)
               .Include(x => x.EstadoOrdenNavigation)
               .Include(x => x.DetalleEquipoOrdenTrabajo).ThenInclude(i => i.ParamEquipoActividadNavigation.EquipoNavigation)
               .Include(x => x.DetalleEquipoOrdenTrabajo).ThenInclude(i => i.ParamEquipoActividadNavigation.ActividadNavigation)
               .Include(x => x.DetalleEquipoOrdenTrabajo).ThenInclude(i => i.UsuarioRegistraNavigation)
               .Include(x => x.DetalleEquipoOrdenTrabajo).ThenInclude(i => i.MovimientoEquipoNavigation)
               .Include(x => x.DetalleMaterialOrdenTrabajo).ThenInclude(i => i.ParamMaterialActividadNavigation.MaterialNavigation)
               .Include(x => x.DetalleMaterialOrdenTrabajo).ThenInclude(i => i.ParamMaterialActividadNavigation.ActividadNavigation)
               .Include(x => x.DetalleMaterialOrdenTrabajo).ThenInclude(i => i.UsuarioRegistraNavigation)
               .Include(x => x.CarpetaNavigation)
               .Include(x => x.UsuarioRegistraNavigation)
               .Include(x => x.SuscriptorNavigation)
               .ToListAsync();          
        }
        public List<LogDetalleEquipoOrdenTrabajo> GetWorkOrderEquipmentLogByID(Guid idDetalle)
        {
            return _context.LogDetalleEquipoOrdenTrabajo.Where(x => x.DetalleEquipoOrdenTRabajo.Equals(idDetalle))
               .Include(x => x.ParamEquipoActividadNavigation.EquipoNavigation)
               .Include(x => x.MovimientoEquipoNavigation)               
               .Include(x => x.DetalleEquipoOrdenTRabajoNavigation.OrdenTrabajoNavigation.CarpetaNavigation)
               .Include(x => x.DetalleEquipoOrdenTRabajoNavigation.OrdenTrabajoNavigation.UsuarioRegistraNavigation)
               .Include(x => x.DetalleEquipoOrdenTRabajoNavigation.OrdenTrabajoNavigation.EstadoOrdenNavigation)
               .Include(x => x.UsuarioModificaNavigation)               
               .ToList();
        }
        public List<LogDetalleMaterialOrdenTrabajo> GetWorkOrderMaterialLogByID(Guid idDetalle)
        {
            return _context.LogDetalleMaterialOrdenTrabajo.Where(x => x.DetalleMaterialOrdenTrabajo.Equals(idDetalle))
               .Include(x => x.ParamMaterialActividadNavigation.MaterialNavigation)               
               .Include(x => x.DetalleMaterialOrdenTrabajoNavigation.OrdenTrabajoNavigation.CarpetaNavigation)
               .Include(x => x.DetalleMaterialOrdenTrabajoNavigation.OrdenTrabajoNavigation.UsuarioRegistraNavigation)
               .Include(x => x.DetalleMaterialOrdenTrabajoNavigation.OrdenTrabajoNavigation.EstadoOrdenNavigation)
               .Include(x => x.UsuarioModificaNavigation)
               .ToList();
        }
        public async Task<List<OrdenTrabajo>> GetWorkOrderBilling(DateTime fechaInicio, DateTime fechaFinal)
        {
            return await _context.OrdenTrabajo.Where(x => x.FechaOrden.Date >= fechaInicio.Date && x.FechaOrden.Date <= fechaFinal.Date && x.EstadoOrdenNavigation.Codigo.Equals(CODIGO_ESTADO_EXITOSA))
               .Include(x => x.EstadoOrdenNavigation)
               .Include(x => x.DetalleEquipoOrdenTrabajo).ThenInclude(i => i.ParamEquipoActividadNavigation.ActividadNavigation)
                .Include(x => x.DetalleEquipoOrdenTrabajo).ThenInclude(i => i.ParamEquipoActividadNavigation.EquipoNavigation)
               .Include(x => x.DetalleMaterialOrdenTrabajo).ThenInclude(i => i.ParamMaterialActividadNavigation.ActividadNavigation)
               .Include(x => x.DetalleMaterialOrdenTrabajo).ThenInclude(i => i.ParamMaterialActividadNavigation.MaterialNavigation)
               .Include(x => x.UsuarioRegistraNavigation.CentroOperacionNavigation)
               .Include(x => x.TecnicoAuxiliarNavigation)
               .Include(x => x.SuscriptorNavigation)
               .ToListAsync();
        }
        public async Task<List<DetalleEquipoOrdenTrabajo>> GetDetailEquipmentByOrder(Guid order)
        {
            return await _context.DetalleEquipoOrdenTrabajo.Where(x => x.OrdenTrabajo == order && x.Activo)
               .Include(x => x.MovimientoEquipoNavigation)
               .Include(x => x.ParamEquipoActividadNavigation.EquipoNavigation)             
               .Include(x => x.ParamEquipoActividadNavigation.ActividadNavigation.CarpetaNavigation)
               .Include(x => x.UsuarioRegistraNavigation)               
               .ToListAsync();
        }
        public async Task<List<DetalleMaterialOrdenTrabajo>> GetDetailMaterialByOrder(Guid order)
        {
            return await _context.DetalleMaterialOrdenTrabajo.Where(x => x.OrdenTrabajo == order && x.Activo)              
               .Include(x => x.ParamMaterialActividadNavigation.MaterialNavigation)
               .Include(x => x.ParamMaterialActividadNavigation.ActividadNavigation.CarpetaNavigation)
               .Include(x => x.UsuarioRegistraNavigation)
               .ToListAsync();
        }
        public async Task<List<MovimientoEquipo>> GetAllMovimientoEquipment()
        {
            return await _context.MovimientoEquipo.ToListAsync();
        }
        /// <summary>
        ///     actualiza un equipo
        /// </summary>
        /// <author>Ariel Bejarano</author>
        /// <param name="user">objeto para Actualizar un usuario</param>
        public void UpdateDetailEquipment(DetalleEquipoOrdenTrabajo detailUpdate)
        {
            var existDetail = _context.DetalleEquipoOrdenTrabajo.Where(x => x.ID.Equals(detailUpdate.ID)).FirstOrDefault();
            detailUpdate.OrdenTrabajo = existDetail.OrdenTrabajo;         
            detailUpdate.FechaHoraRegistra = DateTime.Now;
            //Log de modificacion.
            AddLogDetailEquipment(existDetail, detailUpdate);
            //modificacion
            _context.Entry(existDetail).CurrentValues.SetValues(detailUpdate);            
            _context.SaveChanges();
        }
        /// <summary>
        ///     actualiza un detalel de equipo
        /// </summary>
        /// <author>Ariel Bejarano</author>
        /// <param name="user">objeto para Actualizar un usuario</param>
        public void UpdateDetailMaterial(DetalleMaterialOrdenTrabajo detailUpdate)
        {
            var existDetail = _context.DetalleMaterialOrdenTrabajo.Where(x => x.ID.Equals(detailUpdate.ID)).FirstOrDefault();
            detailUpdate.OrdenTrabajo = existDetail.OrdenTrabajo;           
            detailUpdate.FechaHoraRegistra = DateTime.Now;
            //Log de modificacion.
            AddLogDetailMaterial(existDetail, detailUpdate);
            //modificacion
            _context.Entry(existDetail).CurrentValues.SetValues(detailUpdate);
            _context.SaveChanges();
        }

        /// <summary>
        /// Obtiene todos las actividades
        /// </summary>
        /// <returns></returns>
        /// <author>Diego Molina</author>
        public async Task<List<Actividad>> GetActivity(Guid? carpeta)
        {
            return await _context.Actividad.Where(x => ((carpeta != null)? x.Carpeta.Equals(carpeta): x.Carpeta != Guid.Empty)  && x.Activo).ToListAsync();
        }

        /// <summary>
        /// Obtiene todos los tipos de foto
        /// </summary>
        /// <returns></returns>
        /// <author>Diego Molina</author>
        public async Task<List<TipoImagen>> GetPhotoType()
        {
            return await _context.TipoImagen.ToListAsync();
        }

        /// <summary>
        ///     actualiza un equipo
        /// </summary>
        /// <author>Ariel Bejarano</author>
        /// <param name="user">objeto para adicionar un log de modificacion de equipos</param>
        public void AddLogDetailEquipment(DetalleEquipoOrdenTrabajo detailexist, DetalleEquipoOrdenTrabajo detailUpdate)
        {
            LogDetalleEquipoOrdenTrabajo   log = new LogDetalleEquipoOrdenTrabajo();
            log.ID = Guid.NewGuid();
            log.DetalleEquipoOrdenTRabajo = detailexist.ID;
            log.ParamEquipoActividad = detailexist.ParamEquipoActividad;
            log.Activo = detailexist.Activo;
            log.FechaHoraModifica = detailexist.FechaHoraRegistra??DateTime.Now;
            log.MovimientoEquipo = detailexist.MovimientoEquipo;    
            log.Serial= detailexist.Serial;
            log.UsuarioModifica = detailUpdate.UsuarioRegistra??Guid.Empty;
            log.ObservacionModifica = detailUpdate.ObservacionModifica;
            _context.LogDetalleEquipoOrdenTrabajo.Add(log);         
        }
        /// <summary>
        ///     actualiza un equipo
        /// </summary>
        /// <author>Ariel Bejarano</author>
        /// <param name="user">objeto para adicionar un log de modificacion de materiales</param>
        public void AddLogDetailMaterial(DetalleMaterialOrdenTrabajo detailexist,DetalleMaterialOrdenTrabajo detailUpdate)
        {
            LogDetalleMaterialOrdenTrabajo log = new LogDetalleMaterialOrdenTrabajo();
            log.ID = Guid.NewGuid();
            log.DetalleMaterialOrdenTrabajo = detailexist.ID;
            log.Cantidad = detailexist.Cantidad;
            log.Activo = detailexist.Activo;
            log.FechaHoraModifica = detailexist.FechaHoraRegistra;
            log.ParamMaterialActividad = detailexist.ParamMaterialActividad;          
            log.UsuarioModifica = detailUpdate.UsuarioRegistra;
            log.ObservacionModifica = detailUpdate.ObservacionModifica;
            _context.LogDetalleMaterialOrdenTrabajo.Add(log);
        }

        #endregion|
    }
}

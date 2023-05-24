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
               .Include(x => x.DetalleEquipoOrdenTrabajo).ThenInclude(i => i.MovimientoEquipoNavigation)
               .Include(x => x.DetalleMaterialOrdenTrabajo).ThenInclude(i => i.ParamMaterialActividadNavigation.MaterialNavigation)
               .Include(x => x.CarpetaNavigation)
               .Include(x => x.UsuarioRegistraNavigation)
               .Include(x => x.SuscriptorNavigation)
               .ToListAsync();          
        }
        public async Task<List<OrdenTrabajo>> GetWorkOrderBilling(DateTime fechaInicio, DateTime fechaFinal)
        {
            return await _context.OrdenTrabajo.Where(x => x.FechaOrden.Date >= fechaInicio.Date && x.FechaOrden.Date <= fechaFinal.Date && x.EstadoOrdenNavigation.Codigo.Equals(CODIGO_ESTADO_EXITOSA))
               .Include(x => x.EstadoOrdenNavigation)
               .Include(x => x.DetalleEquipoOrdenTrabajo).ThenInclude(i => i.ParamEquipoActividadNavigation.ActividadNavigation)               
               .Include(x => x.DetalleMaterialOrdenTrabajo).ThenInclude(i => i.ParamMaterialActividadNavigation.ActividadNavigation)               
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
            detailUpdate.UsuarioRegistra = existDetail.UsuarioRegistra;
            detailUpdate.FechaHoraRegistra = existDetail.FechaHoraRegistra;          
            _context.Entry(existDetail).CurrentValues.SetValues(detailUpdate);
            //Log de modificacion.
            AddLogDetailEquipment(existDetail);
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
            detailUpdate.UsuarioRegistra = existDetail.UsuarioRegistra;
            detailUpdate.FechaHoraRegistra = existDetail.FechaHoraRegistra;        
            _context.Entry(existDetail).CurrentValues.SetValues(detailUpdate);
            _context.SaveChanges();
        }

        /// <summary>
        /// Obtiene todos las actividades
        /// </summary>
        /// <returns></returns>
        /// <author>Diego Molina</author>
        public async Task<List<Actividad>> GetActivity()
        {
            return await _context.Actividad.ToListAsync();
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
        public void AddLogDetailEquipment(DetalleEquipoOrdenTrabajo detailexist)
        {
            LogDetalleEquipoOrdenTrabajo   log = new LogDetalleEquipoOrdenTrabajo();
            log.ID = Guid.NewGuid();
            log.DetalleEquipoOrdenTRabajo = detailexist.ID;
            log.ParamEquipoActividad = detailexist.ParamEquipoActividad;
            log.Activo = detailexist.Activo;
            log.FechaHoraModifica = DateTime.Now;
            log.MovimientoEquipo = detailexist.MovimientoEquipo;    
            log.Serial= detailexist.Serial;
            log.UsuarioModifica = detailexist.UsuarioRegistra??Guid.Empty;
            _context.LogDetalleEquipoOrdenTrabajo.Add(log);         
        }

        #endregion|
    }
}

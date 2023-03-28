﻿using DomainServices.Domain.Contracts.WorkOrderManagement;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelcosAppApi.DataAccess.Entities;

namespace DomainServices.Domain.WorkOrderManagement
{
    public class WorkOrderManagementDomain : IWorkOrderManagementDomain
    {
        private readonly TelcosSuiteContext _context;

        public WorkOrderManagementDomain(TelcosSuiteContext telcosApiContext)
        {
            _context = telcosApiContext;
        }

        #region Method

        public async Task<List<OrdenTrabajo>> GetWorkOrderByUser(Guid? user)
        {
            return await _context.OrdenTrabajo.Where(x => x.UsuarioRegistra.Equals(user))
                .Include(x => x.EstadoOrdenNavigation)
                .Include(x => x.SuscriptorNavigation).ThenInclude(x => x.TipoSuscriptorNavigation)
                .ToListAsync();
        }

        public async Task<List<TipoSuscriptor>> GetSubscriberType()
        {
            return await _context.TipoSuscriptor.Where(x => x.Activo).ToListAsync();
        }

        public async Task<List<EstadoOrdenTrabajo>> GetWorkOrderStatus()
        {
            return await _context.EstadoOrdenTrabajo.Where(x => x.Activo).ToListAsync();
        }

        /// <summary>
        ///     Guarda una orden de trabajo.
        /// </summary>
        /// <author>Ariel Bejarano</author>
        /// <param name="workOrder">objeto para guardar orden de trabajo</param>
        public void SaveWorkOrder(OrdenTrabajo workOrder)
        {

            _context.OrdenTrabajo.Add(workOrder);
            _context.SaveChanges();
        }

        public async Task<OrdenTrabajo> GetWorkOrderById(Guid Id)
        {
            return await _context.OrdenTrabajo.Where(x => x.ID.Equals(Id))
                .Include(x => x.EstadoOrdenNavigation)
                .Include(x => x.DetalleEquipoOrdenTrabajo)
                .Include(x => x.DetalleMaterialOrdenTrabajo)
                .Include(x => x.SuscriptorNavigation).ThenInclude(x => x.TipoSuscriptorNavigation)
                .FirstOrDefaultAsync();
        }

        public void SaveDetalleEquipoOrdenTrabajo(ICollection<DetalleEquipoOrdenTrabajo> detalleEquipoOrdenTrabajo)
        {

            _context.DetalleEquipoOrdenTrabajo.AddRange(detalleEquipoOrdenTrabajo);
        }

        public void SaveDetalleMaterialOrdenTrabajo(ICollection<DetalleMaterialOrdenTrabajo> detalleMaterialOrdenTrabajo)
        {

            _context.DetalleMaterialOrdenTrabajo.AddRange(detalleMaterialOrdenTrabajo);
        }

        public void SaveChanges()
        {

            _context.SaveChanges();

        }

        #endregion|


    }
}

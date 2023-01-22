﻿using DomainServices.Domain.Contracts.WorkOrderManagement;
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
                .Include(x => x.SuscriptorNavigation)
                .ToListAsync();
        }
        /// <summary>
        ///     Guarda una orden de trabajo.
        /// </summary>
        /// <author>Ariel Bejarano</author>
        /// <param name="workOrder">objeto para guardar orden de trabajo</param>
        public  void SaveWorkOrder(OrdenTrabajo workOrder)
        {     
           
            _context.OrdenTrabajo.Add(workOrder);
            _context.SaveChanges();
        }
        #endregion|


    }
}

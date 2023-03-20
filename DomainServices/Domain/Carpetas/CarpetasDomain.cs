using DomainServices.Domain.Contracts.Carpetas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelcosAppApi.DataAccess.Entities;

namespace DomainServices.Domain.Carpetas
{
    public class CarpetasDomain : ICarpetaDomain
    {
        private readonly TelcosSuiteContext _context;

        public CarpetasDomain(TelcosSuiteContext telcosApiContext)
        {
            _context = telcosApiContext;
        }
        #region Method
        public async Task<List<Carpeta>> GetCarpetas()
        {
            return await _context.Carpeta.ToListAsync();
           
        }

        public async Task<List<ParamEquipoActividad>> GetActyvitiEquipmentByFile(Guid file)
        {
            return await _context.ParamEquipoActividad.Where(x => x.ActividadNavigation.Carpeta.Equals(file) && x.Activo)
                                                       .Include(x => x.ActividadNavigation)
                                                       .Include(x => x.EquipoNavigation).ToListAsync();
        }
        public async Task<List<ParamMaterialActividad>> GetActyvitiMaterialByFile(Guid file)
        {
            return await _context.ParamMaterialActividad.Where(x => x.ActividadNavigation.Carpeta.Equals(file) && x.Activo)
                                                       .Include(x => x.ActividadNavigation)
                                                       .Include(x => x.MaterialNavigation).ToListAsync();
        }
        #endregion|
    }
}

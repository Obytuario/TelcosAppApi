using DomainServices.Domain.Contracts.CentroOperaciones;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelcosAppApi.DataAccess.Entities;

namespace DomainServices.Domain.CentroOperaciones
{
    public class CentroOperacionesDomain : ICentroOperacionesDomain
    {
        private readonly TelcosSuiteContext _context;

        public CentroOperacionesDomain(TelcosSuiteContext telcosApiContext)
        {
            _context = telcosApiContext;
        }
        #region Method
        public async Task<List<CentroOperacion>> GetCentroOperaciones()
        {
            return await _context.CentroOperacion.ToListAsync();
        }
        #endregion|
    }
}

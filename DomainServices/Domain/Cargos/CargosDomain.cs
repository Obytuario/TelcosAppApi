using DomainServices.Domain.Contracts.cargos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelcosAppApi.DataAccess.Entities;

namespace DomainServices.Domain.Cargos
{
    public class CargosDomain : ICargosDomain
    {
        private readonly TelcosSuiteContext _context;
        public CargosDomain(TelcosSuiteContext telcosApiContext)
        {
            _context = telcosApiContext;
        }

        #region Method
        public async Task<List<Cargo>> GetCargos()
        {
            return await _context.Cargo.ToListAsync();
        }
        #endregion|

    }
}

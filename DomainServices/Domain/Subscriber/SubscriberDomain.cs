using DomainServices.Domain.Contracts.Subscriber;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelcosAppApi.DataAccess.Entities;

namespace DomainServices.Domain.Subscriber
{
    public class SubscriberDomain : ISubscriberDomain
    {
        private readonly TelcosSuiteContext _context;
        public SubscriberDomain(TelcosSuiteContext telcosApiContext)
        {
            _context = telcosApiContext;
        }

        #region Method
        public async Task<List<TipoSuscriptor>> GetAllSubscriberType()
        {
            return await _context.TipoSuscriptor.ToListAsync();
        }
        #endregion|
    }
}

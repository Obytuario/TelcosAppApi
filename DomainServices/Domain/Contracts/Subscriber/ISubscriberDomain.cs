using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelcosAppApi.DataAccess.Entities;

namespace DomainServices.Domain.Contracts.Subscriber
{
    public interface ISubscriberDomain
    {
        Task<List<TipoSuscriptor>> GetAllSubscriberType();
    }
}

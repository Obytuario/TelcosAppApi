using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelcosAppApi.DataAccess.Entities;

namespace DomainServices.Domain.Contracts.Location
{
    public interface ILocationDomain
    {
        void SaveLocationUser(UbicacionUsuario user);
        Task<List<UbicacionUsuario>> GetLocationByUser(Guid user);
        Task<List<OrdenTrabajo>> GetLocationWorkOrder(Guid user);
    }
}

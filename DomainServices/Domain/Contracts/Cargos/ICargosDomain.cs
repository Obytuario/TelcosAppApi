using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelcosAppApi.DataAccess.Entities;

namespace DomainServices.Domain.Contracts.cargos
{
    public interface ICargosDomain
    {
        Task<List<Cargo>> GetCargos();
    }
}

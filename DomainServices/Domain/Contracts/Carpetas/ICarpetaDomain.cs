using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelcosAppApi.DataAccess.Entities;

namespace DomainServices.Domain.Contracts.Carpetas
{
    public interface ICarpetaDomain
    {
        Task<List<Carpeta>> GetCarpetas();
    }
}

using AplicationServices.DTOs.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationServices.Application.Contracts.CentroOperaciones
{
    public interface ICentroOperacionServices
    {
        Task<RequestResult<List<GenericDto>>> GetCentroOperaciones();

    }
}

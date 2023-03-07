using AplicationServices.DTOs.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationServices.Application.Contracts.Cargos
{
    public interface ICargosServices
    {
        Task<RequestResult<List<GenericDto>>> GetCargos();
    }
}

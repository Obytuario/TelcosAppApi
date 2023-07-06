using AplicationServices.DTOs.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationServices.Application.Contracts.DashBoard
{
    public interface IDashBoardServices
    {
        Task<RequestResult<DashBoardOperationsDTO>> GetDashBoard(string code);
    }
}

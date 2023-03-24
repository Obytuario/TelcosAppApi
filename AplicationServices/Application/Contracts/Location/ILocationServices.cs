using AplicationServices.DTOs.Generics;
using AplicationServices.DTOs.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationServices.Application.Contracts.Location
{
    public interface ILocationServices
    {
        Task<RequestResult<string>> SaveLocationUser(LocationDto userDto);
    }
}

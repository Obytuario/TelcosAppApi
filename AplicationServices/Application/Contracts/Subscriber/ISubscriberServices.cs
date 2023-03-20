using AplicationServices.DTOs.Generics;
using AplicationServices.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationServices.Application.Contracts.Subscriber
{
    public interface ISubscriberServices
    {
        Task<RequestResult<List<GenericDto>>> GetAllSubscriberType();
    }
}

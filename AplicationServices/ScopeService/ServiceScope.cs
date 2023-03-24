using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationServices.ScopeService
{
    public class ServiceScope :IServiceScopeDI
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceScope(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public T GetService<T>()
        {
            return _serviceProvider.GetService<T>();
        }
    }
}

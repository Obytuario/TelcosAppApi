using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationServices.ScopeService
{
    public interface IServiceScopeDI
    {
        public T GetService<T>();
    }
}

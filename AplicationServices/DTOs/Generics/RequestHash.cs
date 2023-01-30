using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationServices.DTOs.Generics
{
    public class RequestHash
    {
        public string Hash { get; set; }    
        public byte[] SaltHash { get; set; }
    }
}

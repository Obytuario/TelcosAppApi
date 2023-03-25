using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationServices.DTOs.Generics
{
    public class GenericDto
    {
        public string IdDto { get; set; }    
        public string DescripcionDto { get; set; }
        public string CodigoDto { get; set; }

    }
    public class paramGenericDto
    {
        public string IdParamGenericActividad { get; set; }
        public string NombreGeneric { get; set; }
        public string NombreActividad { get; set; }
        public string CodigoDto { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationServices.DTOs.Location
{
    public class LocationDto
    {
        public Guid IdUser { get; set; }       
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
    }
}

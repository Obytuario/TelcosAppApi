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
    public class GetLocationUserDto
    {
        public Guid IdUser { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public GetLabelUserDto labelOptions { get; set; }   

    }
    public class GetLocationWorkOrderDto
    {
        public Guid IdWork { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public string Tecnico { get; set; }
        public string Suscriptor { get; set; }        
        public GetLabelWorkOrderDto labelOptions { get; set; }

    }
    public class GetLabelUserDto
    {        
        public string Text { get; set; }
        public string Color { get; set; } = "#FFFFFF";
        public string Conection { get; set; } ="Sin conexión el dia de hoy.";

    }
    public class GetLabelWorkOrderDto
    {
        public string Text { get; set; }
        public string Color { get; set; } = "#FFFFFF";
        public string Code { get; set; }

    }
}

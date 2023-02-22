using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationServices.DTOs.workOrderManagement
{
    public class GetWorkOrderManagementDTO
    {
        public Guid IdDto { get; set; }
        public string? NumeroOrdenDto { get; set; }
        public Guid UsuarioRegistraDto { get; set; }
        public Guid EstadoOrdenDTO { get; set; }
        public string? NombreEstadoOrdenDTO { get; set; }
        public Guid IdSuscriptorDto { get; set; }
        public string? NombreSuscriptorDto { get; set; }
        public string? CuentaSuscriptorDto { get; set; }

    }
}

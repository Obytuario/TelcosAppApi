using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationServices.DTOs.User
{
    public class PostUserDto
    {
        public string? PrimerNombreDto { get; set; }
        public string? NumeroDocumentoDto { get; set; }
        public string? ContrasenaDto { get; set; }
        public Guid RolDto { get; set; }
    }
}

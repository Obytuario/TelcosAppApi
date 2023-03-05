using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationServices.DTOs.User
{
    public class PostUserDto
    {
        public Guid? id { get; set; }
        public string? fName { get; set; }
        public string? lName { get; set; }
        public string? numberDocument { get; set; }
        public string? password { get; set; }
        public Guid idRol { get; set; }
        public string? rol { get; set; }
    }
}

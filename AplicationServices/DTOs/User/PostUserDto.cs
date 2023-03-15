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
        public Guid idCharge { get; set; }
        public string? charge { get; set; }
        public Guid? idSuperior { get; set; }
        public string? nameSuperior { get; set; }
        public Guid idOperationCenter { get; set; }
        public bool active { get; set; }
        public string? operationCenter { get; set; }
        public string? mobile { get; set; }
        public string? email { get; set; }
        
    }
}

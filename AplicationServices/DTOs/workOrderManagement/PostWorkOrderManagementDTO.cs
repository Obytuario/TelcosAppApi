

namespace AplicationServices.DTOs.workOrderManagement
{
    public class PostWorkOrderManagementDTO
    {
        public string? NumeroOrdenDto { get; set; }       
        public Guid UsuarioRegistraDto { get; set; }
        public Guid FolderDto { get; set; }
        public Guid? OperationCenterDto { get; set; }
        public string? latitude { get; set; }
        public string? longitude { get; set; }
        public string? Nodo { get; set; }
        public SuscriptorDTO? suscriptorDTO { get; set; }   
    }
    public class SuscriptorDTO
    {
        public string? NombreDTO { get; set; }
        public string? ApellidoDTO { get; set; }
        public Guid TipoSuscriptorDto { get; set; }
        public string? NumeroCuentaDto { get; set; }
        public string? DireccionDto { get; set; }
    }
}

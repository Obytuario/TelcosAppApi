namespace TelcosAppApi.AplicationServices.DTOs.Authentication
{
    public class CredencialesUsuarioDto
    {
        public string? User { get; set; }   
        public string? Password { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
    }
    public class CargaDto
    {
        public List<MaterialesDto> Materiales { get; set; }
        public List<EquiposDto> Equipos { get; set; }
        public List<ActividadesDto> Actividades { get; set; }
       
    }
    public class EquiposDto
    {
        public string? Equipo { get; set; }        
    }
    public class MaterialesDto
    {
        public string? Material { get; set; }       
    }
    public class ActividadesDto
    {
        public string? Actividad { get; set; }       
    }
}

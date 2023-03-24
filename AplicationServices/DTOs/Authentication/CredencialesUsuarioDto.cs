namespace TelcosAppApi.AplicationServices.DTOs.Authentication
{
    public class CredencialesUsuarioDto
    {
        public string? User { get; set; }   
        public string? Password { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
    }
}

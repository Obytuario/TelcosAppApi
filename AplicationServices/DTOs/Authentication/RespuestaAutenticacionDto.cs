namespace TelcosAppApi.AplicationServices.DTOs.Authentication
{
    public class RespuestaAutenticacionDto
    {
        public string? Token { get; set; }
        public Guid UserID { get; set; }
    }
}

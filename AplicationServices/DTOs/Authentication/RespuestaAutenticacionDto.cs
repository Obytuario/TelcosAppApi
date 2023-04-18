namespace TelcosAppApi.AplicationServices.DTOs.Authentication
{
    public class RespuestaAutenticacionDto
    {
        public string? Token { get; set; }
        public Guid UserID { get; set; }
        public string RolCode { get; set; }
        public string NameUser { get; set; }
        public string ChargeUser { get; set; }
    }
}

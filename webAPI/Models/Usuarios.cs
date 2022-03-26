namespace webAPI.Models
{
    public class Usuarios
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}

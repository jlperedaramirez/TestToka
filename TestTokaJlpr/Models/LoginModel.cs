using System.ComponentModel.DataAnnotations;

namespace TestTokaJlpr.Models
{
    public class LoginModel
    {
        private const string email = "El email es obligatorio.";
        private const string pass = "El password es obligatorio.";

        //[Required(ErrorMessage = email)]
        public string Email { get; set; }

        //[Required(ErrorMessage = pass)]
        public string Password { get; set; }

        public int Id { get; set; }
    }
}

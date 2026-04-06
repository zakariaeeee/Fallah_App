using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fallah_App.Models
{
    public class User
    {

        public int? Id { get; set; }
        [Required(ErrorMessage = "Nom est obligatoire")]
        public string Nom { get; set; }
        [Required(ErrorMessage = "Prenom est obligatoire")]
        public string Prenom { get; set; }
        [Required(ErrorMessage = "Login est obligatoire")]
        public string Login { get; set; }
        [Required(ErrorMessage = "mot de pass est obligatoire")]
        public string Password { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "mot de pass de confirmation est obligatoire")]
        public string PasswordConfirmation { get; set; }
        [Required(ErrorMessage = "Email est obligatoire")]
        public string Email { get; set; }
        public DateTime ? Date_Dernier_Auth { get; set; }
        [Required(ErrorMessage = "image est obligatoire")]
        public string? Image { get; set; }
        [NotMapped]
        public IFormFile ? file { get; set; }


    }
}

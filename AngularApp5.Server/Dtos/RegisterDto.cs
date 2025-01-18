using System.ComponentModel.DataAnnotations;

namespace AngularApp5.Server.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^(?=.{6,10}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+{}:;'?/<>])(?!.*\s).*$", 
            ErrorMessage = "Hasło musi zawierać conajmniej 1 dużą i małą literę, 1 cyfrę, 1 znak specjalny oraz mieć minimum 6 znaków")]

        public string Password { get; set; }
    }
}

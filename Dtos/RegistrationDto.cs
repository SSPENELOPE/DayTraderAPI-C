using System.ComponentModel.DataAnnotations;

namespace DayTraderProAPI.Dtos
{
    public class RegistrationDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$",
           ErrorMessage = "Passord Must have Atleast 1 Lower 1 Uppera nd 1 Special Character")]
        public string Password { get; set; }
    }
}

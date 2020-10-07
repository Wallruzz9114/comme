using System.ComponentModel.DataAnnotations;

namespace Core.ViewModels
{
    public class RegisterRequestViewModel
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(
            "(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$",
            ErrorMessage = "Password must have 1 uppercase, 1 lowercase, 1 number, 1 special character and be at least 6 characters long"
        )]
        public string Password { get; set; }
    }
}
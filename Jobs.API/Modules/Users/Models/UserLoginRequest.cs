using System.ComponentModel.DataAnnotations;

namespace Jobs.API.Modules.Users.Models
{
    public class UserLoginRequest
    {
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;
    }
}

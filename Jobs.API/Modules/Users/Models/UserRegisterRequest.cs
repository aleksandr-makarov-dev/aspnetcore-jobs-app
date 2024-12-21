using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Jobs.API.Modules.Users.Models
{
    public class UserRegisterRequest
    {
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}

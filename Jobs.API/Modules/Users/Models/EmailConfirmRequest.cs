using System.ComponentModel.DataAnnotations;

namespace Jobs.API.Modules.Users.Models
{
    public class EmailConfirmRequest
    {
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirmation token is required")]
        public string Token { get; set; } = string.Empty;
    }
}

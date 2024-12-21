using System.ComponentModel.DataAnnotations;

namespace Jobs.API.Modules.Users.Models
{
    public class EmailConfirmationRequest
    {
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}

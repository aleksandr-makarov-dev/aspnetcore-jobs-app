using System.Security.Claims;

namespace Jobs.API.Modules.Users.Models
{
    public record UserLoginResponse(ClaimsIdentity Identity);
}

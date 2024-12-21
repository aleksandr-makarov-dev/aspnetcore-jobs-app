using System.Security.Claims;
using Jobs.API.Helpers;
using Jobs.API.Models;
using Jobs.API.Modules.Users.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Jobs.API.Modules.Users
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController:ExtendedControllerBase
    {
        private readonly UserService _userService;
        private readonly EmailService _emailService;

        public UsersController(UserService userService, EmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUserAsync(UserRegisterRequest request)
        {
            // Attempt to register the user
            var registrationResult = await _userService.RegisterUserAsync(request);
            if (!registrationResult.IsSuccess)
                return HandleResult(registrationResult);

            // Generate email confirmation URL
            var emailConfirmationUrlResult = await _userService.GenerateEmailConfirmationUrlAsync(request.Email);
            if (!emailConfirmationUrlResult.IsSuccess)
                return HandleResult(emailConfirmationUrlResult);

            // Send confirmation email
            await _emailService.SendEmailConfirmationAsync(registrationResult.Value.Email, emailConfirmationUrlResult.Value);

            return Ok(new MessageResponse("Confirm your email address to finish registration"));
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery] EmailConfirmRequest request)
        {
            // Attempt to confirm the email
            var confirmationResult = await _userService.ConfirmEmailAsync(request);

            // Handle the fail result
            if (!confirmationResult.IsSuccess)
                return HandleResult(confirmationResult);


            return Ok(new MessageResponse("Email address confirmed"));
        }

        [HttpPost("resend-confirmation")]
        public async Task<IActionResult> ResendConfirmationEmailAsync(EmailConfirmationRequest request)
        {
            // Generate email confirmation URL
            var emailConfirmationUrlResult = await _userService.GenerateEmailConfirmationUrlAsync(request.Email);

            // Handle the fail result
            if (!emailConfirmationUrlResult.IsSuccess)
                return HandleResult(emailConfirmationUrlResult);

            // Send confirmation email
            await _emailService.SendEmailConfirmationAsync(request.Email, emailConfirmationUrlResult.Value);

            return Ok(new MessageResponse("New confirmation letter is sent"));
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUserAsync(UserLoginRequest request)
        {
            // Attempt to log in the user
            var loginResult = await _userService.LoginUserAsync(request);

            // Handle the fail result
            if (!loginResult.IsSuccess)
                return HandleResult(loginResult);

            // Sign in the user by creating a new cookie
            await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme,
                new ClaimsPrincipal(loginResult.Value.Identity));

            return Ok(new MessageResponse("Logged in"));
        }

    }
}

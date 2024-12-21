using System.Security.Claims;
using FluentResults;
using Jobs.API.Errors;
using Jobs.API.Modules.Users.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Jobs.API.Modules.Users
{
    public class UserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public UserService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Result<UserRegisterResponse>> RegisterUserAsync(UserRegisterRequest request)
        {
            var foundUser = await _userManager.FindByEmailAsync(request.Email);

            if (foundUser is not null)
                return Result.Fail(new BadRequestError("Email address is already registered"));

            var newUser = request.ToUser();

            var createResult = await _userManager.CreateAsync(newUser, request.Password);

            if (!createResult.Succeeded)
                return Result.Fail(new BadRequestError(createResult.Errors.First().Description));

            var addToRoleResult = await _userManager.AddToRoleAsync(newUser, "User");

            if (!addToRoleResult.Succeeded)
                return Result.Fail(new BadRequestError(addToRoleResult.Errors.First().Description));

            return Result.Ok(newUser.ToUserRegisterResponse());
        }

        public async Task<Result<string>> GenerateEmailConfirmationUrlAsync(string email)
        {
            var foundUser = await _userManager.FindByEmailAsync(email);

            if (foundUser is null)
                return Result.Fail(new NotFoundError("User does not exist"));

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(foundUser);
            var confirmationLink = BuildEmailConfirmationUrl(email, token);

            return Result.Ok(confirmationLink);
        }

        public async Task<Result<bool>> ConfirmEmailAsync(EmailConfirmRequest request)
        {
            var foundUser = await _userManager.FindByEmailAsync(request.Email);

            if(foundUser is null)
                return Result.Fail(new NotFoundError("User does not exist"));

            var confirmationResult = await _userManager.ConfirmEmailAsync(foundUser, request.Token);

            if (!confirmationResult.Succeeded)
                return Result.Fail(new BadRequestError(confirmationResult.Errors.First().Description));

            return Result.Ok(true);
        }

        public async Task<Result<UserLoginResponse>> LoginUserAsync(UserLoginRequest request)
        {
            var foundUser = await _userManager.FindByEmailAsync(request.Email);

            if(foundUser is null || !await _userManager.CheckPasswordAsync(foundUser, request.Password))
                return Result.Fail(new UnauthorizedError("Invalid email or password"));

            if(!await _userManager.IsEmailConfirmedAsync(foundUser))
                return Result.Fail(new UnauthorizedError("Email address is not confirmed"));

            var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, foundUser.Id.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Name, foundUser.Email));

            return Result.Ok(new UserLoginResponse(identity));
        }

        // Helper to build email confirmation URL
        private string BuildEmailConfirmationUrl(string email, string token)
        {
            return $"https://localhost:7264/api/users/confirm-email?email={email}&token={token}";
        }
    }
}

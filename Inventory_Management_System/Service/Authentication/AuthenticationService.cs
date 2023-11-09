using Microsoft.AspNetCore.Identity;

namespace Inventory_Management_System.Service.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenService _tokenService;

        public AuthenticationService(UserManager<IdentityUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<AuthenticationResult> RegisterAsync(string email, string username, string password)
        {
            var result = await _userManager.CreateAsync(
                new IdentityUser { UserName = username, Email = email }, password);

            if (!result.Succeeded)
            {
                return FailedRegistration(result, email, username);
            }

            return new AuthenticationResult(true, email, username, "");
        }

        private static AuthenticationResult FailedRegistration(IdentityResult result, string email, string username)
        {
            var authResult = new AuthenticationResult(false, email, username, "");

            foreach (var error in result.Errors)
            {
                authResult.ErrorMessages.Add(error.Code, error.Description);
            }

            return authResult;
        }

        public async Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            var managedUser = await _userManager.FindByEmailAsync(email);

            if (managedUser == null)
            {
                return InvalidEmail(email);
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, password);
            if (!isPasswordValid)
            {
                return InvalidPassword(email, managedUser.UserName);
            }

            var accessToken = _tokenService.CreateToken(managedUser);

            return new AuthenticationResult(true, managedUser.Email, managedUser.UserName, accessToken);
        }

        private static AuthenticationResult InvalidEmail(string email)
        {
            var result = new AuthenticationResult(false, email, "", "");
            result.ErrorMessages.Add("Bad credentials", "Invalid email");
            return result;
        }

        private static AuthenticationResult InvalidPassword(string email, string userName)
        {
            var result = new AuthenticationResult(false, email, userName, "");
            result.ErrorMessages.Add("Bad credentials", "Invalid password");
            return result;
        }
    }
}

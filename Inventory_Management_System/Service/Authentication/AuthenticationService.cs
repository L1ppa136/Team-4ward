using Microsoft.AspNetCore.Identity;

namespace Inventory_Management_System.Service.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AuthenticationService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
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
    }
}

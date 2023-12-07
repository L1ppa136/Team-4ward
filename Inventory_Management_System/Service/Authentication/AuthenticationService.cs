using Inventory_Management_System.Contracts;
using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;

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

        public async Task<AuthenticationResult> RegisterAsync(string email, string username, string password, string role)
        {
            var user = new IdentityUser { UserName = username, Email = email };
            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                return FailedRegistration(result, email, username);
            }
            await SetRole(user.UserName, role);

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

        public async Task<AuthenticationResult> LoginAsync(string username, string password)
        {
            var managedUser = await _userManager.FindByNameAsync(username);

            if (managedUser == null)
            {
                return InvalidUsername(username);
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, password);
            if (!isPasswordValid)
            {
                return InvalidPassword(managedUser.Email, managedUser.UserName);
            }
            
            var roles = await _userManager.GetRolesAsync(managedUser);
            var accessToken = _tokenService.CreateToken(managedUser, roles.Last());

            return new AuthenticationResult(true, managedUser.Email, managedUser.UserName, accessToken);
        }

        private static AuthenticationResult InvalidUsername(string username)
        {
            var result = new AuthenticationResult(false, "", username, "");
            result.ErrorMessages.Add("Bad credentials", "Invalid username");
            return result;
        }

        private static AuthenticationResult InvalidPassword(string email, string userName)
        {
            var result = new AuthenticationResult(false, email, userName, "");
            result.ErrorMessages.Add("Bad credentials", "Invalid password");
            return result;
        }

        public async Task<AuthenticationResult> SetRole(string username, string role)
        {
            var managedUser = await _userManager.FindByNameAsync(username);
            if (managedUser == null)
            {
                return InvalidUsername(username);
            }
            var roles = await _userManager.GetRolesAsync(managedUser);
            await _userManager.RemoveFromRolesAsync(managedUser, roles);
            await _userManager.AddToRoleAsync(managedUser, role);

            return new AuthenticationResult(true, managedUser.Email, managedUser.UserName, "");
        }

        public async Task<IList<string>> GetRoles(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var roles = await _userManager.GetRolesAsync(user);
            // var role = roles[0];
            return roles;
        }
    }
}

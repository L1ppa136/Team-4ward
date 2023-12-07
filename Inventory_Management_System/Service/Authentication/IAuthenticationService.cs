using Inventory_Management_System.Contracts;

namespace Inventory_Management_System.Service.Authentication
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResult> RegisterAsync(string email, string username, string password, string role);

        Task<AuthenticationResult> LoginAsync(string username, string password);

        Task<AuthenticationResult> SetRole(string username, string role);
        Task<IList<string>> GetRoles(string userName);
    }
}

namespace Inventory_Management_System.Service.Authentication
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResult> RegisterAsync(string email, string username, string password);

        Task<AuthenticationResult> LoginAsync(string username, string password);
    }
}

using Microsoft.AspNetCore.Identity;

namespace Inventory_Management_System.Service.Authentication
{
    public interface ITokenService
    {
        string CreateToken(IdentityUser user, string? role);
    }
}

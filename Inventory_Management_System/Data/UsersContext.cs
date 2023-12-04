using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Management_System.Data
{
    public class UsersContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public UsersContext(DbContextOptions<UsersContext> options) : base(options)
        {
        }
    }
}

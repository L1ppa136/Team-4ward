using Inventory_Management_System.Model.Folder;
using Inventory_Management_System.Service.UserRights;
using Microsoft.AspNetCore.Identity;

namespace Inventory_Management_System.Model
{
    public class Admin : User, IAdmin, ICustomerPlanner, IForkliftDriver, IProductionLeader, IShiftLeader
    {
        public Admin(string userName, string password) : base(userName, password)
        {
            Role = Role.Admin;
        }

        public void ModifyUserEssentials(User user, string? name, string? password)
        {
            throw new NotImplementedException();
        }
    }
}

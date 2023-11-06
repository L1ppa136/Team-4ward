using Inventory_Management_System.Model;

namespace Inventory_Management_System.Service.UserRights
{
    public interface IAdmin
    {
        void ModifyUserEssentials(int id, string? username, string? password);


    }
}

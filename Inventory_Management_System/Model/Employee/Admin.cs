using Inventory_Management_System.Model.Folder;
using Inventory_Management_System.Service.Repositories;
using Inventory_Management_System.Service.UserRights;
using Microsoft.AspNetCore.Identity;

namespace Inventory_Management_System.Model.Employee
{
    //NOT USED, TO BE DELETED
    public class Admin : User, IAdmin, ICustomerPlanner, IForkliftDriver, IProductionLeader, IShiftLeader
    {
        private readonly IUserRepository _userRepository;
        public Admin(string userName, string password, IUserRepository userRepository) : base(userName, password)
        {
            Role = Role.Admin;
            _userRepository = userRepository;
        }

        public async void ModifyUserEssentials(int id, string? username, string? password)
        {
            var user = await _userRepository.GetById(id);
            user.UserName = username;
            user.Password = password;
            _userRepository.Update(user);
        }
    }
}

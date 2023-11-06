using Inventory_Management_System.Model;
using Inventory_Management_System.Model.Folder;

namespace Inventory_Management_System.Service.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetById(int id);

        Task<User?> GetByName(string name);

        Task<List<User>> GetUsersByRole(Role role);

        Task<List<User>> GetAllUsers();
        void Add(User user);

        void Update(User user);

        void Delete(User user);
    }
}

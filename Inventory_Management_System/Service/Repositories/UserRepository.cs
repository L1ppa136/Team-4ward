using Inventory_Management_System.Model;
using Inventory_Management_System.Model.Folder;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Management_System.Service.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly InventoryManagementDBContext _dbContext;

        public UserRepository(InventoryManagementDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async void Add(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async void Delete(User user)
        {
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User?> GetById(int id)
        {
            User? user = await _dbContext.Users.FindAsync(id);
            return user;
        }

        public async Task<User?> GetByName(string name)
        {
            User? user = await _dbContext.Users.FirstOrDefaultAsync(u=> u.UserName == name);
            return user;
        }

        public async Task<List<User>> GetUsersByRole(Role role)
        {
            return await _dbContext.Users.Where(u => u.Role == role).ToListAsync();
        }

        public async void Update(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}

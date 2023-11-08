using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Inventory_Management_System.Model.Folder;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Management_System.Model.Employee
{
    public abstract class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public Role Role { get; set; }

        public User(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}

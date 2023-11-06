using Azure.Identity;

namespace Inventory_Management_System.Model.InputModel
{
    public class UserInput
    {
        public int Id { get; set; } 
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}

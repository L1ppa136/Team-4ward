using System.ComponentModel.DataAnnotations;

namespace Inventory_Management_System.Contracts
{
    public record SetRoleRequest([Required] string UserName, [Required] string Role);
}

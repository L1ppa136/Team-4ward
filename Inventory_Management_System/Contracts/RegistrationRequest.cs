using System.ComponentModel.DataAnnotations;

namespace Inventory_Management_System.Contracts
{
    public record RegistrationRequest([Required] string Email, [Required] string UserName, [Required] string Password);

}

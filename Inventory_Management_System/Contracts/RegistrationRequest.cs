namespace Inventory_Management_System.Contracts
{
    public record RegistrationRequest([Required] string Email, [Required] string Username, [Required] string Password);

}

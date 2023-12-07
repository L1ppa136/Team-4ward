using System.ComponentModel.DataAnnotations;

namespace Inventory_Management_System.Contracts
{
    public record OrderRequest([Required] int Quantity, [Required] string ProductDesignation);
}

using Inventory_Management_System.Model.Enums;

namespace Inventory_Management_System.Service.Repositories
{
    public interface ISupplier
    {
        void CreateRawMaterial(int quantity, ProductDesignation productDesignation);
    }
}

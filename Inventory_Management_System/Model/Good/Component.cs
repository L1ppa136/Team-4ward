using Inventory_Management_System.Model.Enums;

namespace Inventory_Management_System.Model.Good
{
    public class Component : Good
    {
        public Component(ProductDesignation productDesignation, string partNumber) : base(productDesignation, partNumber)
        {
        }
    }
}

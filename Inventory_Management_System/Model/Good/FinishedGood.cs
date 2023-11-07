using Inventory_Management_System.Model.Enums;

namespace Inventory_Management_System.Model.Good
{
    public class FinishedGood : Good
    {
        public Dictionary<Component, int> BuildOfMaterial { get; set; }
        public FinishedGood(Dictionary<Component, int> buildOfMaterial) : base(ProductDesignation.Airbag)
        {
            BuildOfMaterial = new Dictionary<Component, int>();
        }
    }
}

using Inventory_Management_System.Model.Enums;

namespace Inventory_Management_System.Model.Good
{
    public class FinishedGood : Good
    {
        protected FinishedGood() 
        {

        }
        public FinishedGood(Dictionary<ProductDesignation, int> buildOfMaterial) : base(ProductDesignation.Airbag)
        {
            BoxCapacity = 100;
        }
    }
}

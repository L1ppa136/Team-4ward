using Inventory_Management_System.Model.Enums;

namespace Inventory_Management_System.Model.Good
{
    public class FinishedGood : Good
    {

        public FinishedGood() : base(ProductDesignation.Airbag)
        {
            BoxCapacity = 100;
        }
    }
}

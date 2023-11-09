using Inventory_Management_System.Model.Enums;

namespace Inventory_Management_System.Model.Good
{
    public class Component : Good
    {
        private Component()
        {

        }
        public Component(ProductDesignation productDesignation) : base(productDesignation)
        {
            switch(productDesignation)
            {
                case ProductDesignation.Nut:
                    BoxCapacity = 4 * 100;
                    break;
                case ProductDesignation.Screw: 
                    BoxCapacity = 4 * 100; 
                    break;
                default: 
                    BoxCapacity = 100;
                    break;
            }
        }
    }
}

using Inventory_Management_System.Model.Enums;

namespace Inventory_Management_System.Model.Good
{
    public abstract class Good
    {
        public ProductDesignation ProductDesignation { get; set; }
        public int PartNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public int BoxCapacity { get; set; }
        

        public Good(ProductDesignation productDesignation)
        {
            ProductDesignation = productDesignation;
            PartNumber = (int)productDesignation;
            CreatedAt = DateTime.Now;
        }
    }
}
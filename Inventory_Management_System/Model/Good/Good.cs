using Inventory_Management_System.Model.Enums;

namespace Inventory_Management_System.Model.Good
{
    public abstract class Good
    {
        public ProductDesignation ProductDesignation { get; set; }
        public string PartNumber { get; set; }
        public DateTime? CreatedAt { get; set; }

        public Good(ProductDesignation productDesignation, string partNumber)
        {
            ProductDesignation = productDesignation;
            PartNumber = partNumber;
            CreatedAt = DateTime.Now;
        }
    }
}
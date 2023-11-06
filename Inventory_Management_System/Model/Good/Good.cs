using Inventory_Management_System.Model.Enums;

namespace Inventory_Management_System.Model.Good
{
    public abstract class Good
    {
        public ComponentType ComponentType { get; set; }
        public string PartNumber { get; set; }
        public DateTime? CreatedAt { get; set; }

        public Good(ComponentType componentType, string partNumber)
        {
            ComponentType = componentType;
            PartNumber = partNumber;
            CreatedAt = DateTime.Now;
        }
    }
}
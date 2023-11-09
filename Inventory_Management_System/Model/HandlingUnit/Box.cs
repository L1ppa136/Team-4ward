using Inventory_Management_System.Model.Good;
using Inventory_Management_System.Model.Location;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory_Management_System.Model.HandlingUnit
{
    public class Box<T> where T : Good.Good
    {
        public int Id { get; set; }
        public int PartNumber { get; set; }
        public DateTime CreatedAt { get; set; }

        [ForeignKey(nameof(RawMaterialLocation))]
        public Guid LocationId { get; set; }
        public Tuple<T, int> Content { get; set; }
        public int MaxCapacity {get; set; }

        public Box(T good, int quantity)
        {
            Content = new Tuple<T, int>(good, quantity);
            MaxCapacity = good.BoxCapacity;
        }
    }
}

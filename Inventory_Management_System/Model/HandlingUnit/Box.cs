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
        public string LocationId { get; set; }
        public T Good { get; set; }
        public int Quantity { get; set; }
        public int MaxCapacity {get; set; }

        protected Box()
        {

        }
        public Box(T good, int quantity)
        {
            Good = good;
            Quantity = quantity;
            MaxCapacity = good.BoxCapacity;
            SetPartNumber(good.PartNumber);
            SetCreatedAt(good.CreatedAt);
        }

        public void SetLocationID(string locationID)
        {
            LocationId = locationID;
        }

        private void SetPartNumber(int partNumber) 
        {
            PartNumber = partNumber;
        }

        private void SetCreatedAt(DateTime createdAt)
        {
            CreatedAt = createdAt;
        }
    }
}

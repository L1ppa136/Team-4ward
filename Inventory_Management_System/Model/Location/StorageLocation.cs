using System.Collections;
using Inventory_Management_System.Model.Enums;

namespace Inventory_Management_System.Model.Location
{
    public abstract class StorageLocation<T>
    {
        public Guid Id { get; set; }
        public string LocationId
        {
            get
            {
                return $"{StorageLine}-{StoragePosition}-{StorageStare}";
            }
        }

        public int StorageLine { get; set; }
        public int StoragePosition { get; set; }
        public int  StorageStare { get; set; }

        
        public LocationType LocationType { get; set; }
        
        public Queue<Tuple<T, int>> Boxes { get; set; }
        public int PartNumber { get; set; }

        public StorageLocation()
        {
           
        }

        public abstract void FillGoods(T good, int quantity);
        public abstract void RemoveBoxes(T good, int quantity);
        protected abstract void SetPartNumber(T good);
        protected abstract void ClearPartNumber();

    }
}

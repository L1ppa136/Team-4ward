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
        public List<T> GoodList { get; set; }
        public int PartNumber { get; set; }
        public DateTime StockCreated { get; set; }

        public StorageLocation()
        {
           
        }

        public abstract void FillGoods(T good, int quantity);
        public abstract void RetrieveComponents(T good, int quantity);
        protected abstract void SetPartNumber(T good);
        protected abstract void SetDate(DateTime date);
        protected abstract void ClearDate();
        protected abstract void ClearPartNumber();

    }
}

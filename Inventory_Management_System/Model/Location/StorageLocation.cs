using Inventory_Management_System.Model.Enums;

namespace Inventory_Management_System.Model.Location
{
    public abstract class StorageLocation
    {
        public string LocationId { get; set; }
        public LocationType LocationType { get; set; }

        public StorageLocation(LocationType locationType) 
        {

        }
    }
}

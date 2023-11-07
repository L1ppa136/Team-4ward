using Inventory_Management_System.Model.Enums;
using Inventory_Management_System.Model.Good;

namespace Inventory_Management_System.Model.Location
{
    public class RawMaterialLocation : StorageLocation
    {
        public List<Component> Components;
        public string PartNumber;
        public DateTime? InboundDate;
        public RawMaterialLocation(LocationType locationType) : base(locationType)
        {
            Components = new List<Component>();
            PartNumber = "";
        }

        public void FillComponents(Component component, int quantity)
        {
            PartNumber = component.PartNumber;
            InboundDate = component.CreatedAt;
            for(int i = 0; i < quantity; i++)
            {
                Components.Add(component);
            }
        }

        public void RetrieveComponents(Component component, int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                Components.Remove(component);
            }
            if(Components.Count == 0) 
            {
                PartNumber = "";
                InboundDate = null;
            }
        }
    }
}

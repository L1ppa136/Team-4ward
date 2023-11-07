using Inventory_Management_System.Model.Enums;
using Inventory_Management_System.Model.Good;

namespace Inventory_Management_System.Model.Location
{
    public class RawMaterialLocation : StorageLocation<Component>
    {
        public RawMaterialLocation()
        {
            LocationType = LocationType.RawMaterial;
            GoodList = new List<Component>(); // ToTuple
        }
        
        public override void FillGoods(Component component, int quantity)
        {
            //0 exception handling
            SetPartNumber(component);
            SetDate(component.CreatedAt);
            
            for (int i = 0; i < quantity; i++)
            {
                GoodList.Add(component);
            }
        }

        public override void RetrieveComponents(Component component, int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                GoodList.Remove(component);
            }

            if (GoodList.Count <= 0)
            {
                ClearDate();
                ClearPartNumber();
            }
        }

        protected override void SetPartNumber(Component good)
        {
            PartNumber = good.PartNumber;
        }

        protected override void SetDate(DateTime date)
        {
            StockCreated = date;
        }
        
        protected override void ClearDate()
        {
            StockCreated = DateTime.MinValue;
        }
        
        protected override void ClearPartNumber()
        {
            PartNumber = 0;
        }
        
    }
}

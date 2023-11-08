using Inventory_Management_System.Model.Enums;
using Inventory_Management_System.Model.Good;

namespace Inventory_Management_System.Model.Location
{
    public class RawMaterialLocation : StorageLocation<Component>
    {
        public List<Tuple<Component, int>> BoxesForProduction { get; set; }
        public RawMaterialLocation()
        {
            LocationType = LocationType.RawMaterial;
            Boxes = new Queue<Tuple<Component, int>>();
        }
        
        public override void FillGoods(Component component, int quantity)
        {
            //0 exception handling
            SetPartNumber(component);
            for (int i = 0; i < quantity/component.BoxCapacity; i++)
            {
                Boxes.Enqueue(new Tuple<Component, int>(component, component.BoxCapacity));
            }
        }

        public override void RemoveBoxes(Component component, int quantity)
        {
            for (int i = 0; i < quantity/component.BoxCapacity; i++)
            {
                Boxes.Enqueue(Boxes.First()); // -> where should we add the moved boxes?
                Boxes.Dequeue();
            }

            if (Boxes.Count <= 0)
            {
                ClearPartNumber();
            }
        }

        protected override void SetPartNumber(Component good)
        {
            PartNumber = good.PartNumber;
        }
        
        protected override void ClearPartNumber()
        {
            PartNumber = 0;
        }
        
    }
}

using Inventory_Management_System.Model.Enums;
using Inventory_Management_System.Model.Good;

namespace Inventory_Management_System.Model.Location
{
    public class RawMaterialLocation : StorageLocation<Component>
    {
        public RawMaterialLocation() 
        {
            LocationType = LocationType.RawMaterial;
        }

        public override void FillGoods(Component good, int quantity)
        {
            base.FillGoods(good, quantity);
        }

        public override Queue<Tuple<Component,int>> RemoveBoxes(Component good, int quantity)
        {
            return base.RemoveBoxes(good, quantity);
        }
    }
}

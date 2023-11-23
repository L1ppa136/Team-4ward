using Inventory_Management_System.Model.Enums;
using Inventory_Management_System.Model.Good;
using Inventory_Management_System.Model.HandlingUnit;

namespace Inventory_Management_System.Model.Location
{
    public class ComponentLocation : StorageLocation<Component>
    {
        private ComponentLocation() { }
        public ComponentLocation(string locationID) : base(locationID, 20)
        {
            LocationType = LocationType.RawMaterial;
        }

        public override void FillGoods(Component good, int quantity)
        {
            base.FillGoods(good, quantity);
        }

        public override List<Box<Component>> RemoveBoxes(Component good, int quantity)
        {
            return base.RemoveBoxes(good, quantity);
        }
    }
}

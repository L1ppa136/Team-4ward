using Inventory_Management_System.Model.Enums;
using Inventory_Management_System.Model.Good;
using Inventory_Management_System.Model.HandlingUnit;

namespace Inventory_Management_System.Model.Location
{
    public class RawMaterialLocation : StorageLocation<Component>
    {
        private RawMaterialLocation() { }
        public RawMaterialLocation(int storageLine, int storagePosition, int storageStare) : base(storageLine, storagePosition, storageStare)
        {
            LocationType = LocationType.RawMaterial;
        }

        public override void FillGoods(Component good, int quantity)
        {
            base.FillGoods(good, quantity);
        }

        public override Queue<Box<Component>> RemoveBoxes(Component good, int quantity)
        {
            return base.RemoveBoxes(good, quantity);
        }
    }
}

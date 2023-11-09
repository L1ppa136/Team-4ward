using System.Collections;
using Inventory_Management_System.Model.Enums;
using Inventory_Management_System.Model.Good;
using Inventory_Management_System.Model.HandlingUnit;

namespace Inventory_Management_System.Model.Location;

public class OutboundLocation : StorageLocation<FinishedGood>
{

    public OutboundLocation(int storageLine, int storagePosition, int storageStare) : base(storageLine, storagePosition, storageStare)
    {
        LocationType = LocationType.FinishedGood;
    }

    public override void FillGoods(FinishedGood good, int quantity)
    {
        base.FillGoods(good, quantity);
    }

    public override Queue<Box<FinishedGood>> RemoveBoxes(FinishedGood good, int quantity)
    {
        return base.RemoveBoxes(good, quantity);
    }
}
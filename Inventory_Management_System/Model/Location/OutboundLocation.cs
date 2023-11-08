using System.Collections;
using Inventory_Management_System.Model.Enums;
using Inventory_Management_System.Model.Good;

namespace Inventory_Management_System.Model.Location;

public class OutboundLocation : StorageLocation<FinishedGood>
{

    public OutboundLocation()
    {
        LocationType = LocationType.FinishedGood;
    }

    public override void FillGoods(FinishedGood good, int quantity)
    {
        base.FillGoods(good, quantity);
    }

    public override Queue<Tuple<FinishedGood, int>> RemoveBoxes(FinishedGood good, int quantity)
    {
        return base.RemoveBoxes(good, quantity);
    }
}
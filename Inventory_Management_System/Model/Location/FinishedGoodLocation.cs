using System.Collections;
using Inventory_Management_System.Model.Enums;
using Inventory_Management_System.Model.Good;
using Inventory_Management_System.Model.HandlingUnit;

namespace Inventory_Management_System.Model.Location;

public class FinishedGoodLocation : StorageLocation<FinishedGood>
{
    private FinishedGoodLocation() { }
    public FinishedGoodLocation(string locationID) : base(locationID, 10)
    {
        LocationType = LocationType.FinishedGood;
    }

    public override void FillGoods(FinishedGood good, int quantity)
    {
        base.FillGoods(good, quantity);
    }

    public override List<Box<FinishedGood>> RemoveBoxes(FinishedGood good, int quantity)
    {
        return base.RemoveBoxes(good, quantity);
    }
}
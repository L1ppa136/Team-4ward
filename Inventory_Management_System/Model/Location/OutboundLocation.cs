using System.Collections;
using Inventory_Management_System.Model.Enums;
using Inventory_Management_System.Model.Good;

namespace Inventory_Management_System.Model.Location;

public class OutboundLocation : StorageLocation<FinishedGood>
{
    
    
    public OutboundLocation() : base()
    {
        LocationType = LocationType.FinishedGood;
        Boxes = new Queue<Tuple<FinishedGood, int>>();
    }

    public override void FillGoods(FinishedGood finishedGood, int quantity)
    {
        //0 exception handling
        SetPartNumber(finishedGood);
        for (int i = 0; i < quantity/finishedGood.BoxCapacity; i++)
        {
            Boxes.Add(new Tuple<FinishedGood, int>(finishedGood, finishedGood.BoxCapacity));
        }
    }

    public override void RemoveBoxes(FinishedGood finishedGood, int quantity)
    {
        int quantityCounter;
        List<Tuple<FinishedGood, int>> BoxesOrderedByDate = Boxes.OrderBy(b=>b.Item1.CreatedAt).ToList();

        for (int i = 0; i < finishedGood.BoxCapacity; i++)
        {
            BoxesOrderedByDate.Remove(BoxesOrderedByDate.First());
        }

        if (Boxes.Count <= 0)
        {
            ClearPartNumber();
        }
    }

    protected override void SetPartNumber(FinishedGood finishedGood)
    {
        PartNumber = finishedGood.PartNumber;
    }
    
        
    protected override void ClearPartNumber()
    {
        PartNumber = 0;
    }
}
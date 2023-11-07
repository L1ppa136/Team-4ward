using Inventory_Management_System.Model.Enums;
using Inventory_Management_System.Model.Good;

namespace Inventory_Management_System.Model.Location;

public class OutboundLocation : StorageLocation<FinishedGood>
{
    
    
    public OutboundLocation() : base()
    {
        LocationType = LocationType.FinishedGood;
        GoodList = new List<FinishedGood>();
    }

    public override void FillGoods(FinishedGood finishedGood, int quantity)
    {
        SetPartNumber(finishedGood);
        SetDate(finishedGood.CreatedAt);
            
        for (int i = 0; i < quantity; i++)
        {
            GoodList.Add(finishedGood);
        }
    }

    public override void RetrieveComponents(FinishedGood finishedGood, int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            GoodList.Remove(finishedGood);
        }

        if (GoodList.Count <= 0)
        {
            ClearDate();
            ClearPartNumber();
        }
    }

    protected override void SetPartNumber(FinishedGood finishedGood)
    {
        PartNumber = finishedGood.PartNumber;
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
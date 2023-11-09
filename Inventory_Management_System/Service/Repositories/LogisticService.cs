using Inventory_Management_System.Data;
using Inventory_Management_System.Model.Enums;
using Inventory_Management_System.Model.Good;
using Inventory_Management_System.Model.HandlingUnit;
using Inventory_Management_System.Model.Location;

namespace Inventory_Management_System.Service.Repositories;

public class LogisticService : IStock, IProduction, ISupplier
{
    private readonly InventoryManagementDBContext _dbContext;

    public LogisticService(InventoryManagementDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void CreateRawMaterial(int quantity, ProductDesignation productDesignation)
    {
        Component component = new Component(productDesignation);
        //RawMaterialLocation rawMaterialLocation = new RawMaterialLocation(1, 1, 1);
        //_dbContext.RawMaterialLocations.Add(rawMaterialLocation);
        //rawMaterialLocation.FillGoods(component,quantity);
        //foreach(var box in rawMaterialLocation.Boxes)
        //{
        //    _dbContext.ComponentStock.Add(box);
        //}
        Box<Component> box = new Box<Component>(component, quantity);
        _dbContext.ComponentStock.Add(box);
        _dbContext.SaveChanges();
    }

    public async Task<List<OutboundLocation>> GetEmptyFinishedGoodLocations()
    {
        List<OutboundLocation> emptyLocations = _dbContext.OutboundLocations.ToList();
        return emptyLocations;
    }

    public Task<List<RawMaterialLocation>> GetEmptyRawMaterialLocations()
    {
        throw new NotImplementedException();
    }

    public Task<List<OutboundLocation>> GetFinishedGoodStock(ProductDesignation productDesignation)
    {
        throw new NotImplementedException();
    }

    public Task<List<RawMaterialLocation>> GetRawMaterialStock(ProductDesignation productDesignation)
    {
        throw new NotImplementedException();
    }

    public void MoveFinishedGoodToOutbound(Queue<Tuple<FinishedGood, int>> finishedGoodQueue)
    {
        throw new NotImplementedException();
    }

    public void MoveRawMaterialToProduction(int quantity, ProductDesignation productDesignation)
    {
        throw new NotImplementedException();
    }

    public Queue<Tuple<FinishedGood, int>> Produce(Queue<Tuple<Component, int>> queue)
    {
        throw new NotImplementedException();
    }
}
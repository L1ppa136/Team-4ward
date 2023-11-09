using Inventory_Management_System.Data;
using Inventory_Management_System.Model.Enums;
using Inventory_Management_System.Model.Good;
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
        throw new NotImplementedException();
    }

    public Task<List<OutboundLocation>> GetEmptyFinishedGoodLocations()
    {
        throw new NotImplementedException();
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
using Inventory_Management_System.Data;
using Inventory_Management_System.Model.Enums;
using Inventory_Management_System.Model.Good;
using Inventory_Management_System.Model.HandlingUnit;
using Inventory_Management_System.Model.Location;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Management_System.Service.Repositories;

public class LogisticService : IStock, IProduction, ISupplier
{
    private readonly InventoryManagementDBContext _dbContext;

    public LogisticService(InventoryManagementDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    // would be nice to return ID to use as response in controller
    public async void CreateRawMaterialAsync(int quantity, ProductDesignation productDesignation)
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
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<OutboundLocation>> GetEmptyFinishedGoodLocationsAsync()
    {
        List<OutboundLocation> emptyLocations = await _dbContext.OutboundLocations.ToListAsync();
        return emptyLocations;
    }

    public async Task<List<RawMaterialLocation>> GetEmptyRawMaterialLocationsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<List<OutboundLocation>> GetFinishedGoodStockAsync(ProductDesignation productDesignation)
    {
        throw new NotImplementedException();
    }

    public async Task<List<RawMaterialLocation>> GetRawMaterialStockAsync(ProductDesignation productDesignation)
    {
        throw new NotImplementedException();
    }

    public async void MoveFinishedGoodToOutboundAsync(Queue<Tuple<FinishedGood, int>> finishedGoodQueue)
    {
        throw new NotImplementedException();
    }

    public async void MoveRawMaterialToProductionAsync(int quantity, ProductDesignation productDesignation)
    {
        throw new NotImplementedException();
    }

    public Task<Queue<Tuple<FinishedGood, int>>> ProduceAsync(Queue<Tuple<Component, int>> queue)
    {
        throw new NotImplementedException();
    }
}
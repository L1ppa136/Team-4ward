using Inventory_Management_System.Data;
using Inventory_Management_System.Model.Enums;
using Inventory_Management_System.Model.Good;
using Inventory_Management_System.Model.HandlingUnit;
using Inventory_Management_System.Model.Location;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Management_System.Service.Repositories;

public class LogisticService : IStock, ISupplier
{
    private readonly InventoryManagementDBContext _dbContext;
    private readonly IProduction _production;

    public LogisticService(InventoryManagementDBContext dbContext, IProduction production)
    {
        _dbContext = dbContext;
        _production = production;
    }

    //public void CreateStorageLocations()
    //{
    //    //Create Raw material locations
    //    for (int i = 1; i < 10; i++)
    //    {
    //        for (int j = 1; j < 10; j++)
    //        {
    //            for (int k = 1; k < 6; k++)
    //            {
    //                var rawMaterialLocation = new RawMaterialLocation($"{i}-{j}-{k}");
    //                _dbContext.RawMaterialLocations.Add(rawMaterialLocation);
    //                _dbContext.SaveChanges();
    //            }
    //        }
    //    }

    //    //Create Finished good material locations
    //    for (int i = 10; i < 20; i++)
    //    {
    //        for (int j = 1; j < 10; j++)
    //        {
    //            for (int k = 1; k < 6; k++)
    //            {
    //                var outboundLocation = new OutboundLocation($"{i}-{j}-{k}");
    //                _dbContext.OutboundLocations.Add(outboundLocation);
    //                _dbContext.SaveChanges();
    //            }
    //        }
    //    }
                
    //}

    // Rules: Each location must be filled completely (until Full == true)
    public async Task CreateRawMaterialAsync(int quantity, ProductDesignation productDesignation)
    {
        Component component = new(productDesignation);
        var numberOfBoxes = quantity / component.BoxCapacity;
        int neededLocations = quantity 
        var emptyLocations = GetEmptyRawMaterialLocationsAsync();
        
        

        await _dbContext.SaveChangesAsync();
    }

public async Task<List<OutboundLocation>> GetEmptyFinishedGoodLocationsAsync()
    {
        List<OutboundLocation> emptyLocations = await _dbContext.OutboundLocations.Where(l => l.Boxes.Count == 0).ToListAsync();
        return emptyLocations;
    }

    public async Task<List<RawMaterialLocation>> GetEmptyRawMaterialLocationsAsync()
    {
        List<RawMaterialLocation> emptyLocations = await _dbContext.RawMaterialLocations.Where(l => l.Boxes.Count == 0).ToListAsync();
        return emptyLocations;
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
}
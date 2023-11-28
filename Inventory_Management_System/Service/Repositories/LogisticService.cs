using Inventory_Management_System.Data;
using Inventory_Management_System.Model.Enums;
using Inventory_Management_System.Model.Good;
using Inventory_Management_System.Model.HandlingUnit;
using Inventory_Management_System.Model.Location;
using Microsoft.EntityFrameworkCore;
using System.Resources;

namespace Inventory_Management_System.Service.Repositories;

public class LogisticService : IStock, ISupplier
{
    private readonly InventoryManagementDBContext _dbContext;
    //private readonly IProduction _production;
    private readonly Dictionary<ProductDesignation, int> _buildOfMaterial = new Dictionary<ProductDesignation, int>() {
            { ProductDesignation.Screw, 4},
            { ProductDesignation.Nut, 4 },
            { ProductDesignation.Cushion, 1},
            { ProductDesignation.Diffusor, 1 },
            { ProductDesignation.Retainer, 1},
            { ProductDesignation.Cover, 1 },
            { ProductDesignation.Emblem, 1 },
            { ProductDesignation.Inflator , 1 },
            { ProductDesignation.WireHarness, 1 }
    };

    public LogisticService(InventoryManagementDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void CreateStorageLocations()
    {
        //throw new NotImplementedException();
        //Create Raw material locations
        for (int i = 1; i < 10; i++)
        {
            for (int j = 1; j < 10; j++)
            {
                for (int k = 1; k < 6; k++)
                {
                    var componentLocation = new ComponentLocation($"{i}-{j}-{k}");
                    _dbContext.ComponentLocations.Add(componentLocation);
                    _dbContext.SaveChanges();
                }
            }
        }

        //Create Finished good material locations
        for (int i = 10; i < 20; i++)
        {
            for (int j = 1; j < 10; j++)
            {
                for (int k = 1; k < 6; k++)
                {
                    var finishedGoodLocation = new FinishedGoodLocation($"{i}-{j}-{k}");
                    _dbContext.FinishedGoodLocations.Add(finishedGoodLocation);
                    _dbContext.SaveChanges();
                }
            }
        }

        //Create productionlocations
        var enumArray = Enum.GetNames(typeof(ProductDesignation));
        for(int i = 0; i < enumArray.Length; i++)
        {
            var productionLocation = new ProductionLocation(enumArray[i].ToString());
            _dbContext.ProductionLocations.Add(productionLocation);
            _dbContext.SaveChanges();
        }
    }

    // Rules: Each location must be filled completely (until Full == true)
    public async Task CreateRawMaterialAsync(int quantity, ProductDesignation productDesignation)
    {
        var emptyLocations = await GetEmptyRawMaterialLocationsAsync();
        Component component = new(productDesignation);
        var locationsToRemove = new List<ComponentLocation>();
        foreach (var location in emptyLocations)
        {
            var fillingQuantity = Math.Min(quantity, component.BoxCapacity * location.MaxBoxCapacity);
            location.FillGoods(component, fillingQuantity);
            quantity -= fillingQuantity;

            if (location.Full)
            {
                locationsToRemove.Add(location);
            }

            if (quantity <= 0)
            {
                break;
            }
        }
        foreach(var location in locationsToRemove) 
        {
            emptyLocations.Remove(location);
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<FinishedGoodLocation>> GetEmptyFinishedGoodLocationsAsync()
    {
        List<FinishedGoodLocation> emptyLocations = await _dbContext.FinishedGoodLocations.Where(l => !l.Full).ToListAsync();
        return emptyLocations;
    }

    public async Task<List<ComponentLocation>> GetEmptyRawMaterialLocationsAsync()
    {
        List<ComponentLocation> emptyLocations = await _dbContext.ComponentLocations.Where(l => !l.Full).ToListAsync();
        return emptyLocations;
    }

    public async Task<List<FinishedGoodLocation>> GetFinishedGoodStockAsync()
    {
        List<FinishedGoodLocation> outboundLocations = await _dbContext.FinishedGoodLocations.Where(l => l.Full).ToListAsync();
        return outboundLocations;
    }

    public async Task<List<ComponentLocation>> GetRawMaterialStockAsync(ProductDesignation productDesignation)
    {
        List<ComponentLocation> rawMaterialLocations = await _dbContext.ComponentLocations
            .Include(l => l.Boxes)
            .Where(l => l.Full && l.PartNumber == (int)productDesignation)
            .ToListAsync();
        return rawMaterialLocations;
    }

    //public async Task<Component> GetComponentByDesignation(ProductDesignation productDesignation)
    //{
    //    return await ;
    //}

    //public async Task<List<Box<FinishedGood>>> ProduceAsnyc(int orderedQuantity)
    //{
    //    if(await ResourcesAvailable(orderedQuantity))
    //    {
    //        var productionLocations = await GetProductionLocations();
    //        foreach (var material in _buildOfMaterial)
    //        {
    //            foreach (var location in productionLocations)
    //            {
    //                location.Quantity -= material.Value;
    //                if(location.Quantity <= 0)
    //                {
    //                    var component = await GetComponentByDesignation(material.Key);
    //                }
    //            }
    //        }
    //    }
    //    return new List<Box<FinishedGood>>();
    //}

    private async Task<bool> ResourcesAvailable(int orderedQuantity)
    {
        var productionLocations = await GetProductionLocations();
        int resources = 0;
        foreach(var material in _buildOfMaterial)
        {
            foreach(var location in productionLocations)
            {
                if(location.LocationName == material.Key.ToString())
                {
                    resources += location.Quantity / material.Value;
                }
            }
        }
        return resources >= orderedQuantity;
    }

    public async Task MoveFinishedGoodToOutboundAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<ProductionLocation> GetProductionLocationByComponent(ProductDesignation componentDesignation)
    {
        ProductionLocation productionLocation = await _dbContext.ProductionLocations.FirstOrDefaultAsync(p => p.LocationName == componentDesignation.ToString());
        return productionLocation;
    }

    public async Task<List<ProductionLocation>> GetProductionLocations()
    {
        return await _dbContext.ProductionLocations.ToListAsync();
    }

    public async Task MoveRawMaterialToProductionAsync(ProductDesignation productDesignation, int quantity)
    {
        var rawMaterialStock = await GetRawMaterialStockAsync(productDesignation);
        List<Box<Component>> neededComponents = new List<Box<Component>>();
        var locationsToEmpty = new List<ComponentLocation>();
        var component = new Component(productDesignation);

        foreach (var location in rawMaterialStock)
        {
            var removeQuantity = Math.Min(quantity, component.BoxCapacity * location.MaxBoxCapacity);
            neededComponents.AddRange(location.RemoveBoxes(component, removeQuantity));
            quantity -= removeQuantity;

            if (!location.Full)
            {
                locationsToEmpty.Add(location);
            }

            if (quantity <= 0)
            {
                break;
            }
        }

        var productionLocation = await GetProductionLocationByComponent(productDesignation);
        productionLocation.StoreComponents(neededComponents);
        await _dbContext.SaveChangesAsync();
    }
}
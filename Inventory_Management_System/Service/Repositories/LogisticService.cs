using Inventory_Management_System.Contracts;
using Inventory_Management_System.Data;
using Inventory_Management_System.Model.Enums;
using Inventory_Management_System.Model.Good;
using Inventory_Management_System.Model.HandlingUnit;
using Inventory_Management_System.Model.Location;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Management_System.Service.Repositories;

public class LogisticService : IStock, ISupplier, IProduction
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
        ////Create Raw material locations
        //for (int i = 1; i < 10; i++)
        //{
        //    for (int j = 1; j < 10; j++)
        //    {
        //        for (int k = 1; k < 6; k++)
        //        {
        //            var componentLocation = new ComponentLocation($"{i}-{j}-{k}");
        //            _dbContext.ComponentLocations.Add(componentLocation);
        //            _dbContext.SaveChanges();
        //        }
        //    }
        //}

        ////Create Finished good material locations
        //for (int i = 10; i < 20; i++)
        //{
        //    for (int j = 1; j < 10; j++)
        //    {
        //        for (int k = 1; k < 6; k++)
        //        {
        //            var finishedGoodLocation = new FinishedGoodLocation($"{i}-{j}-{k}");
        //            _dbContext.FinishedGoodLocations.Add(finishedGoodLocation);
        //            _dbContext.SaveChanges();
        //        }
        //    }
        //}

        //Create productionlocations
        var enumArray = Enum.GetNames(typeof(ProductDesignation));
        for (int i = 0; i < enumArray.Length; i++)
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
        List<ComponentLocation> emptyLocations = await _dbContext.ComponentLocations.Where(l => !l.Full && l.PartNumber == 0).ToListAsync();
        return emptyLocations;
    }

    public async Task<List<FinishedGoodLocation>> GetFinishedGoodStockAsync()
    {
        List<FinishedGoodLocation> outboundLocations = await _dbContext.FinishedGoodLocations
            .Include(l=> l.Boxes)
            .Where(l => l.Boxes.Count > 0)
            .OrderBy(l => l.Boxes.Count)
            .ToListAsync();
        return outboundLocations;
    }

    public async Task<List<ComponentLocation>> GetRawMaterialStockAsync(ProductDesignation productDesignation)
    {
        List<ComponentLocation> rawMaterialLocations = await _dbContext.ComponentLocations
            .Include(l => l.Boxes)
            .Where(l => l.PartNumber == (int)productDesignation)
            .OrderBy(l => l.Boxes.Count)
            .ToListAsync();
        return rawMaterialLocations;
    }

    public async Task<List<ComponentLocation>> GetAllRawMaterialStockAsync()
    {
        return await _dbContext.ComponentLocations.Where(l=> l.Boxes.Count > 0).ToListAsync();
    }

    private async Task<List<Box<Component>>> GetUsedUpComponentStock()
    {
        return await _dbContext.ComponentStock.Where(b => b.Quantity <= 0).ToListAsync();
    }

    private async Task<List<Box<Component>>> GetProductionComponentStockAsync(ProductDesignation productDesignation)
    {
        return await _dbContext.ComponentStock.Where(b => b.PartNumber == (int)productDesignation && b.LocationName == $"{productDesignation.ToString()}").ToListAsync();
    }

    private async Task<List<Box<FinishedGood>>> GetEmptiedFinishedGoodProductionLocationAsync()
    {
        return await _dbContext.FinishedGoodStock.Where(b => b.LocationName == "Airbag").ToListAsync();
    }

    public async Task ClearUsedUpComponentStockAsync()
    {
        var usedUpBoxes = await GetUsedUpComponentStock();

        foreach (var box in usedUpBoxes)
        {
            _dbContext.ComponentStock.Remove(box);
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task<ProductionResult> ProduceAsync(int orderedQuantity)
    {
        try
        {
            if (await ResourcesAvailable(orderedQuantity))
            {
                var productionLocations = await GetAllProductionLocationsAsync();
                FinishedGood airbag = new FinishedGood();
                List<Box<FinishedGood>> producedFinishedGoods = new List<Box<FinishedGood>>();

                foreach (var material in _buildOfMaterial)
                {
                    foreach (var location in productionLocations)
                    {
                        if (location.LocationName == material.Key.ToString())
                        {
                            int neededQuantity = orderedQuantity * material.Value;

                            try
                            {
                                location.UseUpComponents(neededQuantity);
                                //int removeFromBoxQuantity = neededQuantity / location.Components.Count();
                                //foreach(var box in componentStock)
                                //{
                                //    box.Quantity -= removeFromBoxQuantity;
                                //    neededQuantity -= removeFromBoxQuantity;
                                //    if(neededQuantity <= 0)
                                //    {
                                //        break;
                                //    }
                                //}
                            }
                            catch (Exception ex)
                            {
                                return new ProductionResult(false, ex.Message);
                            }
                        }
                    }
                }

                for (int i = 0; i < orderedQuantity / airbag.BoxCapacity; i++)
                {
                    Box<FinishedGood> newBox = new Box<FinishedGood>(airbag, airbag.BoxCapacity, "Airbag");
                    producedFinishedGoods.Add(newBox);
                }

                var finishedGoodLocation = await GetProductionLocationByComponentAsync(ProductDesignation.Airbag);
                finishedGoodLocation.StoreFinishedGoods(producedFinishedGoods);
                await _dbContext.SaveChangesAsync();
                return new ProductionResult(true, $"{orderedQuantity} pcs of Airbag produced and can be found on the production line!");
            }
            else
            {
                return new ProductionResult(false, "Quantity cannot be produced!");
            }
        }
        catch (Exception ex)
        {
            // Handle other exceptions if needed
            return new ProductionResult(false, $"Unhandled exception: {ex.Message}");
        }
    }


    private async Task<bool> ResourcesAvailable(int orderedQuantity)
    {
        var productionLocations = await GetAllProductionLocationsAsync();
        foreach(var material in _buildOfMaterial)
        {
            bool isMaterialAvailable = productionLocations
                .Any(l => l.LocationName == material.Key.ToString() && l.Quantity / material.Value >= orderedQuantity);

            if (!isMaterialAvailable)
            {
                return false;
            }
        }
        return true;
    }

    public async Task<ProductionResult> MoveFinishedGoodToOutboundAsync()
    {
        var finishedGoodLocation = await GetProductionLocationByComponentAsync(ProductDesignation.Airbag);
        var emptyFinishedGoodLocations = await GetEmptyFinishedGoodLocationsAsync();
        var locationsToRemove = new List<FinishedGoodLocation>();
        int storedOnLineQuantity = finishedGoodLocation.Quantity;
        if(storedOnLineQuantity <= 0)
        {
            return new ProductionResult(false, $"There is no finished good on production line!");
        }
        var finishedGood = new FinishedGood();
        foreach (var location in emptyFinishedGoodLocations)
        {
            var fillingQuantity = Math.Min(storedOnLineQuantity, finishedGood.BoxCapacity * location.MaxBoxCapacity);
            location.FillGoods(finishedGood, fillingQuantity);
            storedOnLineQuantity -= fillingQuantity;

            if (location.Full)
            {
                locationsToRemove.Add(location);
            }

            if (storedOnLineQuantity <= 0)
            {
                break;
            }
        }

        foreach (var location in locationsToRemove)
        {
            emptyFinishedGoodLocations.Remove(location);
        }
        int movedQuantity = finishedGoodLocation.Quantity;
        var finishedGoodStock = await GetEmptiedFinishedGoodProductionLocationAsync();
        foreach (var box in finishedGoodStock) 
        {
            _dbContext.FinishedGoodStock.Remove(box);
        }
        finishedGoodLocation.ClearFinishedGoods();

        await _dbContext.SaveChangesAsync();
        return new ProductionResult(true, $"{movedQuantity} pcs of Airbag moved from production line to Outbound Area!");
    }

    public async Task<ProductionLocation> GetProductionLocationByComponentAsync(ProductDesignation componentDesignation)
    {
        var productionLocation = await _dbContext.ProductionLocations.FirstOrDefaultAsync(p => p.LocationName == componentDesignation.ToString());
        return productionLocation;
    }

    public async Task<List<ProductionLocation>> GetAllProductionLocationsAsync()
    {
        return await _dbContext.ProductionLocations.Include(l => l.Components).Include(l => l.FinishedGoods).ToListAsync();
    }

    public async Task<ProductionResult> MoveRawMaterialToProductionAsync(ProductDesignation productDesignation, int quantity)
    {
        try
        {
            int returnQuantity = quantity;
            var rawMaterialStock = await GetRawMaterialStockAsync(productDesignation);
            if (rawMaterialStock.Count == 0)
            {
                return new ProductionResult(false, $"There are no {productDesignation.ToString()} in the inbound area, please check inhouse stock!");
            }
            List<Box<Component>> neededComponents = new List<Box<Component>>();
            var locationsToEmpty = new List<ComponentLocation>();
            var component = new Component(productDesignation);

            foreach (var location in rawMaterialStock)
            {
                var removeQuantity = Math.Min(quantity, component.BoxCapacity * location.Boxes.Count); // location.MaxBoxCapacity incorrect, Boxes.Count might work
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

            var productionLocation = await GetProductionLocationByComponentAsync(productDesignation);
            productionLocation.StoreComponents(neededComponents);
            await _dbContext.SaveChangesAsync();
            return new ProductionResult(true, $"{returnQuantity} pcs of {productDesignation.ToString()} moved to production successfully!");
        }catch(Exception ex)
        {
            return new ProductionResult(false, $"{ex.Message}");
        }
        
    }

    public async Task<ProductionResult> DeliverFinishedGoodsToCustomer(int orderQuantity)
    {
        try
        {
            var finishedGoodLocations = await GetFinishedGoodStockAsync();
            if (IsSufficientFinishedGoodStockAvailable(orderQuantity, finishedGoodLocations))
            {
                List<Box<FinishedGood>> neededGoods = new List<Box<FinishedGood>>();
                var finishedGood = new FinishedGood();

                foreach (var location in finishedGoodLocations)
                {
                    var quantity = orderQuantity;
                    var removeQuantity = Math.Min(quantity, finishedGood.BoxCapacity * location.Boxes.Count);
                    neededGoods.AddRange(location.RemoveBoxes(finishedGood, removeQuantity));
                    quantity -= removeQuantity;

                    if (quantity <= 0)
                    {
                        break;
                    }
                }

                await ClearDeliveredFinishedGoodStockAsync(neededGoods);
                await _dbContext.SaveChangesAsync();
                return new ProductionResult(true, $"{orderQuantity} of airbags have been successfully delivered to customer.");
            }
            else
            {
                return new ProductionResult(false, $"There are not enough finished good on stock to fulfill delivery needs.");
            }
        }
        catch (Exception ex)
        {
            return new ProductionResult(false, $"{ex.Message}");
        }

    }

    private bool IsSufficientFinishedGoodStockAvailable(int quantity, List<FinishedGoodLocation> finishedGoodLocations)
    {
        int sum = 0;
        foreach (var location in finishedGoodLocations)
        {
            foreach (var box in location.Boxes)
            {
                sum += box.Quantity;
            }
        }
        return sum >= quantity;
    }

    private async Task ClearDeliveredFinishedGoodStockAsync(List<Box<FinishedGood>> boxesToRemove)
    {
        foreach (var box in boxesToRemove)
        {
            _dbContext.FinishedGoodStock.Remove(box);
        }

        await _dbContext.SaveChangesAsync();
    }
}
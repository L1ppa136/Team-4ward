using Inventory_Management_System.Contracts;
using Inventory_Management_System.Model.Enums;
using Inventory_Management_System.Model.Good;
using Inventory_Management_System.Model.Location;

namespace Inventory_Management_System.Service.Repositories
{
    public interface IStock
    {
        void CreateStorageLocations();
        Task<List<FinishedGoodLocation>> GetFinishedGoodStockAsync(); //ProductDesignation includes PartNumber as well, easier to query
        Task<List<ComponentLocation>> GetRawMaterialStockAsync(ProductDesignation productDesgination);
        Task<List<ComponentLocation>> GetAllRawMaterialStockAsync();
        Task<List<ComponentLocation>> GetEmptyRawMaterialLocationsAsync();
        Task<List<FinishedGoodLocation>> GetEmptyFinishedGoodLocationsAsync();
        Task<ProductionResult> MoveRawMaterialToProductionAsync(ProductDesignation productDesignation, int quantity);
        Task<ProductionResult> MoveFinishedGoodToOutboundAsync();
        Task<ProductionLocation> GetProductionLocationByComponentAsync(ProductDesignation componentDesignation);
        Task<List<ProductionLocation>> GetAllProductionLocationsAsync();
        Task<ProductionResult> DeliverFinishedGoodsToCustomer(int orderQuantity);
        Task ClearUsedUpComponentStockAsync();
    }
}
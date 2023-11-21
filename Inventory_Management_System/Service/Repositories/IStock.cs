using Inventory_Management_System.Model.Enums;
using Inventory_Management_System.Model.Good;
using Inventory_Management_System.Model.Location;

namespace Inventory_Management_System.Service.Repositories
{
    public interface IStock
    {
        void CreateStorageLocations();
        Task<List<OutboundLocation>> GetFinishedGoodStockAsync(); //ProductDesignation includes PartNumber as well, easier to query
        Task<List<RawMaterialLocation>> GetRawMaterialStockAsync(ProductDesignation productDesgination);
        Task<List<RawMaterialLocation>> GetEmptyRawMaterialLocationsAsync();
        Task<List<OutboundLocation>> GetEmptyFinishedGoodLocationsAsync();
        Task MoveRawMaterialToProductionAsync(ProductDesignation productDesignation, int quantity);
        Task MoveFinishedGoodToOutboundAsync();
    }
}

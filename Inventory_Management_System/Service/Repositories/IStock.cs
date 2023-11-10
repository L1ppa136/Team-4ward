using Inventory_Management_System.Model.Enums;
using Inventory_Management_System.Model.Good;
using Inventory_Management_System.Model.Location;

namespace Inventory_Management_System.Service.Repositories
{
    public interface IStock
    {
        Task<List<OutboundLocation>> GetFinishedGoodStockAsync(ProductDesignation productDesignation); //ProductDesignation includes PartNumber as well, easier to query
        Task<List<RawMaterialLocation>> GetRawMaterialStockAsync(ProductDesignation productDesignation);
        Task<List<RawMaterialLocation>> GetEmptyRawMaterialLocationsAsync();
        Task<List<OutboundLocation>> GetEmptyFinishedGoodLocationsAsync();
        void MoveRawMaterialToProductionAsync(int quantity, ProductDesignation productDesignation);
        void MoveFinishedGoodToOutboundAsync(Queue<Tuple<FinishedGood, int>> finishedGoodQueue);
    }
}

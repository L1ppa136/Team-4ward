using Inventory_Management_System.Model.Enums;
using Inventory_Management_System.Model.Good;
using Inventory_Management_System.Model.Location;

namespace Inventory_Management_System.Service.Repositories
{
    public interface IWarehouse
    {
        Task<List<OutboundLocation>> GetFinishedGoodStock(ProductDesignation productDesignation); //ProductDesignation includes PartNumber as well, easier to query
        Task<List<RawMaterialLocation>> GetRawMaterialStock(ProductDesignation productDesignation);
        Task<List<RawMaterialLocation>> GetEmptyRawMaterialLocations();
        Task<List<OutboundLocation>> GetEmptyFinishedGoodLocations();
        void CreateRawMaterial(int quantity, ProductDesignation productDesignation);
        void MoveRawMaterialToProduction(int quantity, ProductDesignation productDesignation);

        void MoveFinishedGoodToOutbound(Queue<Tuple<FinishedGood, int>> finishedGoodQueue);
    }
}

using Inventory_Management_System.Model.Location;

namespace Inventory_Management_System.Service.Repositories
{
    public interface IWarehouse
    {
        Task<List<OutboundLocation>> GetFinishedGoodStock(int partNumber);
        Task<List<RawMaterialLocation>> GetRawMaterialStock(int partNumber);
        Task<RawMaterialLocation> GetEmptyLocation();
        Task<RawMaterialLocation> CreateRawMaterialLocation(RawMaterialLocation rawMaterialLocation);
        Task<RawMaterialLocation> DeleteRawMaterialLocation(int id);
        Task<RawMaterialLocation> UpdateRawMaterialLocation(int id);
        
    }
}

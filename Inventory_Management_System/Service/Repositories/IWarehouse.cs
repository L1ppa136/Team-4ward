using Inventory_Management_System.Model.Location;

namespace Inventory_Management_System.Service.Repositories
{
    public interface IWarehouse<T>
    {
        Task<List<OutboundLocation>> GetFinishedGoodStock(int partNumber);
        Task<List<RawMaterialLocation>> GetRawMaterialStock(int partNumber);
        Task<List<StorageLocation<T>>> GetEmptyLocations();
        Task<List<StorageLocation<T>>> GetAvailableLocations();
        Task<StorageLocation<T>> RetrieveStorageLocation(int id);
        Task<StorageLocation<T>> FillStorageLocation(int id);
    }
}

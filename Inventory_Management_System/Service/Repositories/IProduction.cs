using Inventory_Management_System.Model.Good;
using Inventory_Management_System.Model.HandlingUnit;

namespace Inventory_Management_System.Service.Repositories
{
    public interface IProduction
    {
        Task<Queue<Box<FinishedGood>>> ProduceAsync();
        void StoreComponents(List<Box<Component>> components);
        List<Box<FinishedGood>> MoveFinishedGoodFromLine();
    }
}

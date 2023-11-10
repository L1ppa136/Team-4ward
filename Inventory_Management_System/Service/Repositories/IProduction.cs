using Inventory_Management_System.Model.Good;

namespace Inventory_Management_System.Service.Repositories
{
    public interface IProduction
    {
        Task<Queue<Tuple<FinishedGood,int>>> ProduceAsync(Queue<Tuple<Component,int>> queue);
    }
}

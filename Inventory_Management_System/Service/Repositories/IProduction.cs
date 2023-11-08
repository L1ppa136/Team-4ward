using Inventory_Management_System.Model.Good;

namespace Inventory_Management_System.Service.Repositories
{
    public interface IProduction
    {
        Queue<Tuple<FinishedGood,int>> Produce(Queue<Tuple<Component,int>> queue);
    }
}

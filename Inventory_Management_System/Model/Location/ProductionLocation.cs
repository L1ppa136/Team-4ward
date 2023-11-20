using Inventory_Management_System.Model.Good;
using Inventory_Management_System.Model.HandlingUnit;
using Inventory_Management_System.Service.Repositories;

namespace Inventory_Management_System.Model.Location
{
    public class ProductionLocation : IProduction
    {
        public List<Box<Component>> Components {get;set;}
        public List<Box<FinishedGood>> FinishedGoods {get;set;}

        public ProductionLocation() 
        {
            Components = new List<Box<Component>>();
            FinishedGoods = new List<Box<FinishedGood>>();
        }

        public Task<List<Box<FinishedGood>>> ProduceAsync()
        {
            throw new NotImplementedException();
        }
    }
}

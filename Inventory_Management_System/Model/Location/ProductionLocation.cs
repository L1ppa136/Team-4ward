using Inventory_Management_System.Model.Enums;
using Inventory_Management_System.Model.Good;
using Inventory_Management_System.Model.HandlingUnit;
using Inventory_Management_System.Service.Repositories;

namespace Inventory_Management_System.Model.Location
{
    public class ProductionLocation : IProduction
    {
        private readonly Dictionary<ProductDesignation, int> _buildOfMaterial = new Dictionary<ProductDesignation, int>() {
            { ProductDesignation.Screw, 4},
            { ProductDesignation.Nut, 4 },
            { ProductDesignation.Cushion, 1},
            { ProductDesignation.Diffusor, 1 },
            { ProductDesignation.Retainer, 1},
            { ProductDesignation.Cover, 1 },
            { ProductDesignation.Emblem, 1 },
            { ProductDesignation.Inflator , 1 },
            { ProductDesignation.WireHarness, 1 }
        };

        public List<Box<Component>> Components { get; set; }
        public List<Box<FinishedGood>> FinishedGoods { get; set; }

        public ProductionLocation()
        {
            Components = new List<Box<Component>>();
            FinishedGoods = new List<Box<FinishedGood>>();
        }

        public Task<Queue<Box<FinishedGood>>> ProduceAsync()
        {
            throw new NotImplementedException();
        }

        public void StoreComponents(List<Box<Component>> components)
        {
            Components.AddRange(components);
        }

        public List<Box<FinishedGood>> MoveFinishedGoodFromLine()
        {
            return FinishedGoods;
        }
    }
}

using Inventory_Management_System.Model.Enums;
using Inventory_Management_System.Model.Good;
using Inventory_Management_System.Model.HandlingUnit;
using Inventory_Management_System.Service.Repositories;
using System.Collections.Generic;

namespace Inventory_Management_System.Model.Location
{
    public class ProductionLocation
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

        public int Id { get; set; }
        public int PartNumber { get; set; }
        public string LocationName {get ; set; }
        public int Quantity { get; set; }
        public List<Box<Component>> Components { get; set; }
        public List<Box<FinishedGood>> FinishedGoods { get; set; }

        public ProductionLocation(string locationName)
        {
            LocationName = locationName;
            Components = new List<Box<Component>>();
            FinishedGoods = new List<Box<FinishedGood>>();
        }

        public void StoreComponents(List<Box<Component>> components)
        {
            var partNumber = components.First().PartNumber;
            PartNumber = partNumber;

            var productDesignation = (ProductDesignation)partNumber;
            LocationName = productDesignation.ToString();

            foreach(var box in components)
            {
                Quantity += box.Quantity;
            }
            
            Components.AddRange(components);
            Components = Components.OrderBy(box => box.CreatedAt).ToList();
        }

        public void UseUpComponents(int quantity)
        {
            var quantityCopy = quantity;
            if (Quantity >= quantity)
            {
                var boxesToBeRemoved = new List<Box<Component>>();
                foreach(var box in Components)
                {   
                    var removeQuantity = Math.Min(box.Quantity, quantity);
                    box.Quantity -= removeQuantity;
                    quantity -= removeQuantity;
                    boxesToBeRemoved.Add(box);
                    if(quantity <= 0)
                    {
                        break;
                    }
                }
                Quantity -= quantityCopy;
                Components.RemoveRange(0, boxesToBeRemoved.Count);
            }
            else
            {
                throw new Exception($"Location does not have enough components, we need {quantity - Quantity} pcs of {LocationName}.");
            }

            if (Quantity == 0)
            {
                PartNumber = 0;
            }
        }

        public void StoreFinishedGoods(List<Box<FinishedGood>> finishedGoods)
        {
            var partNumber = finishedGoods.First().PartNumber;
            PartNumber = partNumber;
            var productDesignation = (ProductDesignation)partNumber;
            LocationName = productDesignation.ToString();
            foreach (var box in finishedGoods)
            {
                box.LocationName = LocationName;
            }
            var boxQuantity = finishedGoods.First().Quantity;
            Quantity += finishedGoods.Count * boxQuantity;
            FinishedGoods.AddRange(finishedGoods);
        }

        public void ClearFinishedGoods()
        {
            Quantity = 0;
            PartNumber = 0;
            FinishedGoods.Clear();
        }
    }
}
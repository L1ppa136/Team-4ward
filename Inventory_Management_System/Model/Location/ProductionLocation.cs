﻿using Inventory_Management_System.Model.Enums;
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
                box.LocationName = LocationName;
            }
            var boxQuantity = components.First().Quantity;
            Quantity += components.Count * boxQuantity;
            Components.AddRange(components);
        }

        public List<Box<FinishedGood>> MoveFinishedGoodFromLine()
        {
            return FinishedGoods;
        }

    }
}

using System.Collections;
using Inventory_Management_System.Model.Good;
using Inventory_Management_System.Model.HandlingUnit;
using Inventory_Management_System.Model.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Management_System.Model.Location
{
    public abstract class StorageLocation<T> where T : Good.Good
    {
        public Guid Id { get; set; }
        public string LocationName{get; set; }
        public LocationType LocationType { get; set; }
        public List<Box<T>> Boxes { get; set; }
        public int PartNumber { get; set; }
        public int MaxBoxCapacity { get; set; }
        public bool Full { get; set; }

        protected StorageLocation()
        {
            Full = false;
            Boxes = new List<Box<T>>();

        }
        public StorageLocation(string locationName, int maxBoxCapacity)
        {
            Full = false;
            LocationName = locationName;
            MaxBoxCapacity = maxBoxCapacity;
            Boxes = new List<Box<T>>();
        }

        public virtual void FillGoods(T good, int quantity)
        {
            SetPartNumber(good);

            while (quantity > 0 && Full == false)
            {
                int boxCapacity = Math.Min(quantity, good.BoxCapacity);
                Box<T> newBox = new Box<T>(good, boxCapacity, this.LocationName);
                Boxes.Add(newBox);

                quantity -= boxCapacity;
                if(Boxes.Count >= MaxBoxCapacity)
                {
                    Full = true;
                }
            }
        }

        public virtual List<Box<T>> RemoveBoxes(T good, int quantity)
        {
            int numberOfBoxes = Boxes.Count;
            List<Box<T>> removedBoxes = new List<Box<T>>();
            List<Box<T>> boxesCopy = Boxes.OrderBy(b => b.CreatedAt).ToList();
            for (int i = 0; i < numberOfBoxes; i++)
            {
                Box<T> boxOut = boxesCopy[i];
                int removeQuantity = Math.Min(quantity, boxOut.Quantity);
                if (removeQuantity < boxOut.Quantity)
                {
                    boxOut.Quantity -= removeQuantity;
                }
                removedBoxes.Add(boxOut);
                Boxes.Remove(boxOut);
                
                quantity -= removeQuantity;
                
                if(quantity <= 0)
                {
                    break;
                }

            }
            if(Boxes.Count < MaxBoxCapacity)
            {
                Full = false;
            }
            if(Boxes.Count == 0)
            {
                ClearPartNumber();
            }
            return removedBoxes;
        }
        protected virtual void SetPartNumber(T good)
        {
            PartNumber = good.PartNumber;
        }
        protected virtual void ClearPartNumber()
        {
            PartNumber = 0;
        }
    }
}
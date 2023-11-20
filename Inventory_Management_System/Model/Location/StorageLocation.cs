using System.Collections;
using Inventory_Management_System.Model.Good;
using Inventory_Management_System.Model.HandlingUnit;
using Inventory_Management_System.Model.Enums;

namespace Inventory_Management_System.Model.Location
{
    public abstract class StorageLocation<T> where T : Good.Good
    {
        public Guid Id { get; set; }
        public string LocationId { get; set; }
        //private int StorageLine {get; set;}
        //private int StoragePosition { get; set; }
        //private int StorageStare { get; set; }

        public LocationType LocationType { get; set; }
        
        public Queue<Box<T>> Boxes { get; set; }
        public int PartNumber { get; set; }

        public int MaxBoxCapacity { get; set; }

        public bool Full { get; set; }

        protected StorageLocation()
        {

        }
        public StorageLocation(string locationID, int maxBoxCapacity)
        {
            Full = false;
            LocationId = locationID;
            MaxBoxCapacity = maxBoxCapacity;
            Boxes = new Queue<Box<T>>();
        }

        public virtual void FillGoods(T good, int quantity)
        {
            SetPartNumber(good);

            while (quantity > 0 && Full == false)
            {
                int boxCapacity = Math.Min(quantity, good.BoxCapacity);
                Box<T> newBox = new Box<T>(good, boxCapacity);
                newBox.SetLocationID(LocationId);
                Boxes.Enqueue(newBox);

                quantity -= boxCapacity;
                if(Boxes.Count >= MaxBoxCapacity)
                {
                    Full = true;
                }
            }
        }

        public virtual Queue<Box<T>> RemoveBoxes(T good, int quantity)
        {
            int numberOfBoxes = quantity / good.BoxCapacity;
            Queue<Box<T>> removedBoxes = new Queue<Box<T>>();
            for (int i = 0; i < numberOfBoxes; i++)
            {
                Box<T> boxOut = Boxes.Dequeue();
                removedBoxes.Enqueue(boxOut);
            }
            if(Boxes.Count <= 0)
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

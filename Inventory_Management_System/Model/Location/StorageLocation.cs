using System.Collections;
using Inventory_Management_System.Model.Good;
using Inventory_Management_System.Model.HandlingUnit;
using Inventory_Management_System.Model.Enums;

namespace Inventory_Management_System.Model.Location
{
    public abstract class StorageLocation<T> where T : Good.Good
    {
        public Guid Id { get; set; }
        public string LocationId
        {
            get
            {
                return $"{StorageLine}-{StoragePosition}-{StorageStare}";
            }
        }
        private int StorageLine {get; set;}
        private int StoragePosition { get; set; }
        private int StorageStare { get; set; }

        public LocationType LocationType { get; set; }
        
        public Queue<Box<T>> Boxes { get; set; }
        public int PartNumber { get; set; }

        protected StorageLocation()
        {

        }
        public StorageLocation(int storageLine, int storagePosition, int storageStare)
        {
            StorageLine = storageLine;
            StoragePosition = storagePosition;
            StorageStare = storageStare;
            Boxes = new Queue<Box<T>>();
        }

        public virtual void FillGoods(T good, int quantity) 
        {
            int numberOfBoxes = quantity / good.BoxCapacity;
            SetPartNumber(good);

            for (int i = 0; i < numberOfBoxes; i++)
            {
                Box<T> newBox = new Box<T>(good, good.BoxCapacity);
                Boxes.Enqueue(newBox);
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

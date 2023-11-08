using System.Collections;
using Inventory_Management_System.Model.Good;
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

        public int StorageLine { get; set; }
        public int StoragePosition { get; set; }
        public int  StorageStare { get; set; }

        
        public LocationType LocationType { get; set; }
        
        public Queue<Tuple<T, int>> Boxes { get; set; }
        public int PartNumber { get; set; }

        public StorageLocation()
        {
           Boxes = new Queue<Tuple<T, int>>();
        }

        public virtual void FillGoods(T good, int quantity) 
        {
            int numberOfBoxes = quantity / good.BoxCapacity;
            SetPartNumber(good);

            for (int i = 0; i < numberOfBoxes; i++)
            {
                Boxes.Enqueue(new Tuple<T, int>(good, good.BoxCapacity));
            }
        }
        public virtual Queue<Tuple<T, int>> RemoveBoxes(T good, int quantity)
        {
            int numberOfBoxes = quantity / good.BoxCapacity;
            Queue<Tuple<T,int>> removedBoxes = new Queue<Tuple<T,int>>();
            for (int i = 0; i < numberOfBoxes; i++)
            {
                Tuple<T, int> boxOut = Boxes.Dequeue();
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

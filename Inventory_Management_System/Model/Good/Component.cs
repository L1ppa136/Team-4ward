using Inventory_Management_System.Model.Enums;

namespace Inventory_Management_System.Model.Good
{
    public class Component : Good
    {
        public Component(ComponentType componentType, string partNumber) : base(componentType, partNumber)
        {
        }
    }
}

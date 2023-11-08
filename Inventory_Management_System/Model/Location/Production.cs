using Inventory_Management_System.Model.Good;

namespace Inventory_Management_System.Model.Location;

public class Production
{
    public List<Tuple<Component, int>> ComponentBoxes { get; set; }
    public List<Tuple<FinishedGood, int>> FinishedGoodBoxes { get; set; }
}
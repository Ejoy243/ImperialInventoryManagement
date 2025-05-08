using ImperialInventoryManagement.Models;

namespace ImperialInventoryManagement.ViewModels
{
    public class ItemView
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Min {  get; set; }
        public int Max { get; set; }
        public double Value { get; set; }

        public List<int> SelectedCategoryIds { get; set; } = new List<int>();


    }
}

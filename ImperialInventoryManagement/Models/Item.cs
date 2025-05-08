using System;

namespace ImperialInventoryManagement.Models
{
    public class Item : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<ItemCategory> ItemCategories { get; set; }
        public int Min {  get; set; }
        public int Max { get; set; }
        public double Value { get; set; }
        public bool Active { get; set; }

        public Item() 
        {
            Name = string.Empty;
            Description = string.Empty;
            ItemCategories = new List<ItemCategory>();
            Min = 0;
            Max = 0;
            Value = 0;
            Active = false;
        }
    }
}

namespace ImperialInventoryManagement.Models
{
    public class Category : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<ItemCategory> ItemCategories { get; set; }

        public Category() 
        {
            ItemCategories = new List<ItemCategory>();
        }
    }
}

using ImperialInventoryManagement.Models;

namespace ImperialInventoryManagement.Repos
{
    public class ItemCategoryRepo : RepoBase<ItemCategory>
    {
        public ItemCategoryRepo(IConfiguration configuration) : base(configuration) { }
    }
}

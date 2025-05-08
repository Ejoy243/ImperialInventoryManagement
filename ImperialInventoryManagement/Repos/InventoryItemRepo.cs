using ImperialInventoryManagement.Models;

namespace ImperialInventoryManagement.Repos
{
    public class InventoryItemRepo: RepoBase<InventoryItem>
    {
        public InventoryItemRepo(IConfiguration config) : base(config) { }
    }
}

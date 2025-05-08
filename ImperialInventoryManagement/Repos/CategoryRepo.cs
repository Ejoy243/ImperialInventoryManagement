using ImperialInventoryManagement.Models;

namespace ImperialInventoryManagement.Repos
{
    public class CategoryRepo : RepoBase<Category>
    {
        public CategoryRepo(IConfiguration configuration) : base(configuration) { }
    }
}

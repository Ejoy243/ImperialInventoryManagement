using ImperialInventoryManagement.Models;

namespace ImperialInventoryManagement.Repos
{
    public class OrderRepo : RepoBase<Order>
    {
        public OrderRepo(IConfiguration configuration) : base(configuration) { }
    }
}

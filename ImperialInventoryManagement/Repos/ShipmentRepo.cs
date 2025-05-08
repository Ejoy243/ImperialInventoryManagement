using ImperialInventoryManagement.Models;

namespace ImperialInventoryManagement.Repos
{
    public class ShipmentRepo : RepoBase<Shipment>
    {
        public ShipmentRepo(IConfiguration config) : base(config) { }
    }
}

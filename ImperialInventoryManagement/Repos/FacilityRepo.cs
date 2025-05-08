using ImperialInventoryManagement.Models;


namespace ImperialInventoryManagement.Repos
{
    public class FacilityRepo: RepoBase<Facility>
    {
        public FacilityRepo(IConfiguration config) : base(config) { }
    }
}

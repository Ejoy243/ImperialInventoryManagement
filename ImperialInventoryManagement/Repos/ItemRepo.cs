using ImperialInventoryManagement.Data;
using ImperialInventoryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace ImperialInventoryManagement.Repos
{
    public class ItemRepo: RepoBase<Item>

    {
        public ItemRepo(IConfiguration config) : base(config) 
        {
        
        }

        public ItemRepo(DbContextOptions<ApplicationDbContext> options, IConfiguration config) : base(config) { }

    }
}

using ImperialInventoryManagement.Models;
using ImperialInventoryManagement.Repos;
using Microsoft.EntityFrameworkCore;

namespace ImperialInventoryManagement.Services
{
    public class ItemCategoryService
    {
        private readonly IRepo<ItemCategory> _repo;
        private readonly IConfiguration _config;
        private readonly ILogger _logger;

        private ItemCategory _itemCategory;
        private List<ItemCategory> _itemCategories;


        protected void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ItemCategory>()
                .HasKey(ic => new { ic.ItemId, ic.CategoryId });
        }
        public ItemCategoryService(IRepo<ItemCategory> repo, IConfiguration config, ILogger<ItemCategory> iLogger)
        {
            _repo = repo;
            _config = config;
            _logger = iLogger;
            _itemCategory = new ItemCategory();
            _itemCategories = new List<ItemCategory>();
        }

        public ItemCategory GetItemCategory(int Id)
        {
            try
            {
                _itemCategory = _repo.Search(x => x.Id == Id && !x.IsDeleted)
                    .Include(x => x.Category)
                    .Include(x => x.Item)
                    .FirstOrDefault() ?? new ItemCategory();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error gettinng Item Category with Id: " + Id, ex);
            }
            return _itemCategory;

        }

        public List<ItemCategory> GetItemCategories()
        {
            try
            {
                _itemCategories = _repo.Search().Where(x => !x.IsDeleted).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting all Item Categories " + ex);
            }
            return _itemCategories;
        }

        public void Add(ItemCategory item)
        {
            try
            {
                _repo.Add(item);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error adding Item Category " + ex);
            }
        }
        public void Remove(ItemCategory item)
        {
            try
            {
                _repo.Delete(item);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error removing Item Category " + ex);
            }
        }

        public void Delete(ItemCategory item)
        {
            try
            {
                item.IsDeleted = true;
                _repo.Update(item);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error deleting Item Category " + ex);
            }
        }

        public void UnDelete(ItemCategory item)
        {
            try
            {
                item.IsDeleted = false;
                _repo.Update(item);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error undeleting Item Category " + ex);
            }
        }
    }

}

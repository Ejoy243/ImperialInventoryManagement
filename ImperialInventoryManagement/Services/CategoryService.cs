using ImperialInventoryManagement.Models;
using ImperialInventoryManagement.Repos;
using Microsoft.EntityFrameworkCore;

namespace ImperialInventoryManagement.Services
{
    public class CategoryService
    {


        private readonly IRepo<Category> _IRepo;
        private readonly IConfiguration _config;
        private readonly ILogger _logger;

        private Category _category;
        private List<Category> _categories;

        public CategoryService(IRepo<Category> repo, IConfiguration config, ILogger<CategoryService> ilogger)
        {
            _IRepo = repo;
            _config = config;
            _logger = ilogger;
            _category = new Category();
        }





        public Category GetCategory(int Id)
        {
            try
            {
                _category = _IRepo.Search(x => x.Id == Id && !x.IsDeleted)
                   .Include(x => x.ItemCategories)
                    .FirstOrDefault() ?? new Category();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting Category with Id " + Id, ex);
            }
            return _category;
        }

        public List<Category> GetCategories()
        {
            try
            {
                _categories = _IRepo.Search().Where(x => !x.IsDeleted).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting all Categories " + ex);
            }
            return _categories;
        }

        public List<Category> GetRecycleBin()
        {
            try
            {
                _categories = _IRepo.Search(x => x.IsDeleted == true)
                    
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting the Recylce Bin " + ex);
            }
            return _categories;
        }

        public void Add(Category category)
        {
            try
            {
                _IRepo.Add(category);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error updating the Categories " + ex);
            }
        }

        public void Remove(Category category)
        {
            try
            {
                _IRepo.Delete(category);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error removing the category" + ex);
            }
        }

        public void Delete(Category category)
        {
            try
            {
                category.IsDeleted = true;
                _IRepo.Update(category);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error deleting the Category " + ex);
            }
        }

        public void UnDelete(Category category)
        {
            try
            {
                category.IsDeleted = false;
                _IRepo.Update(category);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error UnDeleting the Category " + ex);
            }
        }



    }
}


using ImperialInventoryManagement.Models;
using ImperialInventoryManagement.Services;
using ImperialInventoryManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ImperialInventoryManagement.Pages
{
    public class CreateCategoryModel : PageModel
    {
        private readonly CategoryService categoryService;
        private readonly ILogger<Category> _logger;

        public Category NewCategory { get; set; }
        [BindProperty]
        public CategoryView Category { get; set; }
        public CreateCategoryModel(CategoryService catserve, ILogger<Category> log)
        {
            categoryService = catserve;
            _logger = log;
            Category = new CategoryView();
            NewCategory = new Category();

        }

        public void OnGet()
        {
            
        }

        public IActionResult OnPost()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    NewCategory.Name = Category.Name;
                    NewCategory.Description = Category.Description;
                    categoryService.Add(NewCategory);
                    _logger.LogInformation("New Category Created");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating category" + ex);
            }
            return LocalRedirect("/");
        }
    }
   
    

}

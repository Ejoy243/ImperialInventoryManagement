using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ImperialInventoryManagement.Services;
using ImperialInventoryManagement.Models;
using ImperialInventoryManagement.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ImperialInventoryManagement.Data;
using System.Linq;

namespace ImperialInventoryManagement.Pages
{
    public class CreateItemModel : PageModel
    {
        private readonly ItemCategoryService _itemCategoryService;
        private readonly CategoryService _categoryService;
        private readonly ItemService _itemService;
        private readonly ILogger<Item> _logger;

        public Item Item { get; set; }
        [BindProperty]
        public ItemView ItemView { get; set; }
        public List<SelectListItem> Categories { get; set; }

        public CreateItemModel(ItemCategoryService itemCategoryService, CategoryService categoryService, ItemService itemService, ILogger<Item> logger)
        {

            _itemCategoryService = itemCategoryService;
            _categoryService = categoryService;
            _itemService = itemService;
            _logger = logger;
            ItemView = new ItemView();

        }
        public void OnGet()
        {
            Categories = _categoryService.GetCategories().Select(c => new SelectListItem {Value=c.Id.ToString(), Text=c.Name  }).ToList();
        }

        

        public IActionResult OnPost()
        {
          
            Item = new Item
            {
                Name = ItemView.Name,
                Description = ItemView.Description,
                Min = ItemView.Min,
                Max = ItemView.Max,
                Value = ItemView.Value,


            };
            _itemService.Add(Item);

            foreach (int id in ItemView.SelectedCategoryIds)
            {
                ItemCategory itemcat = new ItemCategory() {ItemId = Item.Id, CategoryId = id };
                _itemCategoryService.Add(itemcat);
            }
            _logger.LogInformation("Create item {Item}", Item.Name);

            return RedirectToPage("/Index");

        }



    }
}

using ImperialInventoryManagement.Models;
using ImperialInventoryManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ImperialInventoryManagement.Pages
{
    public class FacilityInventoryModel : PageModel
    {
        private readonly ILogger<FacilityInventoryModel> _logger;
        private readonly FacilityService _facilityService;
        private readonly ItemService _itemService;
        private readonly InventoryItemService inventoryItemService;

        public Facility facility { get; set; }
        public List<InventoryItem> items { get; set; }

        public FacilityInventoryModel(ILogger<FacilityInventoryModel> logger, FacilityService facilityService, ItemService itemService, InventoryItemService inventoryItemService)
        {
            _logger = logger;
            _facilityService = facilityService;
            _itemService = itemService;
            this.inventoryItemService = inventoryItemService;
        }

        public IActionResult OnGet(int Id)
        {
            try
            {
                if (Id != null)
                {
                    facility = _facilityService.GetFacility((int)Id);
                    items = inventoryItemService.GetItemsForFacility(facility.Id);

                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
            }
            return Page();
        }


        public Item GetItem(int Id)
        {
            return _itemService.GetItem((int)Id);
        }

        
    }
}

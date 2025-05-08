using ImperialInventoryManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ImperialInventoryManagement.Models;
using Microsoft.CodeAnalysis;
using System.Linq;



namespace ImperialInventoryManagement.Pages
{
    public class StockFacilityModel : PageModel
    {
        private readonly FacilityService facilityService;
        private readonly ItemService itemService;
        private readonly InventoryItemService inventoryItemService;
        private readonly ILogger<StockFacilityModel> logger;

        public Facility Facility { get; set; }
        public List<SelectListItem> Items { get; set; }

        [BindProperty]
        public int SelectedItemId { get; set; }

        [BindProperty]
        public Item Item { get; set; }
        [BindProperty]
        public InventoryItem inventoryItem { get; set; }
        [BindProperty]
        public int Quantity { get; set; }

        public StockFacilityModel(FacilityService facilityService, ItemService itemService, InventoryItemService inventoryItemService, ILogger<StockFacilityModel> logger)
        {
            this.facilityService = facilityService;
            this.itemService = itemService;
            this.inventoryItemService = inventoryItemService;
            this.logger = logger;
            SelectedItemId = 0;
            Quantity = 0;
        }

        public void OnGet(int Id)
        {
            
            Facility = facilityService.GetFacility(Id);
            Items = itemService.GetItems()
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name })
                .ToList();

            if (SelectedItemId != 0)
            {
                Item = itemService.GetItem(SelectedItemId);
                inventoryItem = inventoryItemService.GetFromItemAndFacil(Id, SelectedItemId);
                Quantity = inventoryItem.ItemAmount;
            }
        }

        public void OnPost(int Id)
        {
            Items = itemService.GetItems()
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name })
                .ToList();

            Item = itemService.GetItem(SelectedItemId);
            //itemService.Update(Item);

            inventoryItem = inventoryItemService.GetFromItemAndFacil(Id, SelectedItemId);


        }

        public IActionResult OnPostSubmit(int Id)
        {
            if ((inventoryItem.ItemId != 0) && (inventoryItem.FacilityId != 0))
            {
                Item = itemService.GetItem(SelectedItemId);
                inventoryItem.ItemAmount = Quantity;
                inventoryItem.Item = Item;
                inventoryItemService.Update(inventoryItem);
            }
            else
            {
                inventoryItem = new InventoryItem();
                inventoryItem.ItemAmount = Quantity;
                inventoryItem.ItemId = SelectedItemId;
                inventoryItem.FacilityId = Id;
                inventoryItemService.Add(inventoryItem);
            }
            return LocalRedirect("/FacilityInventory/" + Id);
        }
    }
}


using ImperialInventoryManagement.Services;
using ImperialInventoryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ImperialInventoryManagement.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace ImperialInventoryManagement.Pages
{
    public class CreateOrderModel : PageModel
    {
        private readonly OrderService orderService;
        private readonly FacilityService facilityService;
        private readonly ItemService itemService;
        private readonly InventoryItemService inventoryItemService;
        private readonly ShipmentService shipmentService;
        private readonly ILogger<Order> _logger;
        
        [BindProperty]
        public Order Order { get;  set; }
        public Facility Facility { get; set; }
        public List<SelectListItem> Items { get; set; }
        public InventoryItem inventoryItem { get; set; }
        public Item Item { get; set; }
        [BindProperty]
        public int SelectListItemId { get; set; }
        [BindProperty]
        public int Quantity { get; set; }
        public CreateOrderModel(OrderService orderService, FacilityService facilityService, ItemService itemService, ShipmentService shipmentService, InventoryItemService ivnitemService, ILogger<Order> log)
        {
            this.orderService = orderService;
            this.facilityService = facilityService;
            this.itemService = itemService;
            this.shipmentService = shipmentService;
            inventoryItemService = ivnitemService;
            _logger = log;
            SelectListItemId = 0;
            Quantity = 0;
        }

        public void OnGet(int Id)
        {
            Facility = facilityService.GetFacility(Id);
            Items = itemService.GetItems()
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name })
                .ToList();

            if (SelectListItemId != 0)
            {
                Item = itemService.GetItem(SelectListItemId);
                inventoryItem = inventoryItemService.GetFromItemAndFacil(Id, SelectListItemId);
                Quantity = inventoryItem.ItemAmount;
            }
        }
        public void OnPost(int Id)
        {
            Facility = facilityService.GetFacility(Id);
            Items = itemService.GetItems()
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name })
                .ToList();

            if (SelectListItemId != 0)
            {
                Item = itemService.GetItem(SelectListItemId);
                inventoryItem = inventoryItemService.GetFromItemAndFacil(Id, SelectListItemId);
                Quantity = inventoryItem.ItemAmount;
            }
        }

        public IActionResult OnPostSubmit(int Id)
        {

            inventoryItem = inventoryItemService.GetFromItemAndFacil(Id, SelectListItemId);
            if (inventoryItem == null)
            {
                _logger.LogError("Error getting inventory item for selected item");
                return Page();
            }
            Item = itemService.GetItem(inventoryItem.ItemId);
            if (Item == null)
            {
                _logger.LogError("error getting item");
                return Page();
            }


            Order.InventoryItemId = inventoryItem.Id;
            
            orderService.Add(Order);

            inventoryItem.ItemReserve = inventoryItem.ItemReserve + Quantity;
            inventoryItemService.Update(inventoryItem);

            Shipment shipment = new Shipment { Status = "Requested", OrderId = Order.Id, UserId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? String.Empty };
            shipmentService.Add(shipment);
            return RedirectToPage("Index");
        }
    }
}

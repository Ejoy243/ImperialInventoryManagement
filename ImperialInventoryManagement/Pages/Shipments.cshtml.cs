using ImperialInventoryManagement.Models;
using ImperialInventoryManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ImperialInventoryManagement.Pages
{
    public class ShipmentsModel : PageModel
    {
        private readonly ShipmentService shipmentService;
        private readonly OrderService orderService;
        private readonly FacilityService facilityService;
        private readonly ILogger<ShipmentsModel> logger;

        public List<Shipment> Shipments { get; set; }
        public List<SelectListItem> status {  get; set; }

        public ShipmentsModel(ShipmentService shipmentService, OrderService orderService, FacilityService facilityService, ILogger<ShipmentsModel> logger)
        {
            this.shipmentService = shipmentService;
            this.orderService = orderService;
            this.facilityService = facilityService;
            this.logger = logger;
        }

        public void OnGet()
        {
            try
            {

                Shipments = shipmentService.GetShipments();
                if (Shipments != null)
                {
                    foreach (Shipment s in Shipments)
                    {
                        s.Order = orderService.GetOrder(s.OrderId);
                    }
                    List<string> statuses = new List<string>() { "Lost", "Damaged", "Shipped", "Recieved", "Transit", "Requested", "Incomplete", "Delayed" };
                    status = statuses.Select(x => new SelectListItem { Text = x, Value = x }).ToList();
                }
            }
            catch(Exception ex)
            {
                logger.LogError("Error loading Shipments" + ex);
            }
        }

        public void OnPost()
        {
            
        }

        public IActionResult OnPostUpdateStatus(int shipmentId, string newStatus)
        {

            Shipment shipment = shipmentService.GetShipment(shipmentId);
            if (shipment != null)
            {
                shipment.Status = newStatus;
                shipmentService.Update(shipment);
            }
            return RedirectToPage();

        }  

        public Facility GetFacility(int Id)
        {
            Facility facility = facilityService.GetFacility(Id);
            return facility;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ImperialInventoryManagement.Models;
using ImperialInventoryManagement.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using ImperialInventoryManagement.ViewModels;

namespace ImperialInventoryManagement.Pages
{
    public class AddFacilityModel : PageModel
    {
        private readonly FacilityService facilityService;
        private readonly ILogger<Facility> _logger;

        public Facility NewFacility { get; set; }
        [BindProperty]
        public FacilityView FacilityView {  get; set; }
        public List<SelectListItem> Locations { get; set; }
        public AddFacilityModel(FacilityService facilityServ, ILogger<Facility> log) 
        {
            facilityService = facilityServ;
            _logger = log;
            NewFacility = new Facility();
            FacilityView = new FacilityView();

        }

        public void OnGet()
        {
            List<string> locations = new List<string> { "Hoth", "Tatooine", "Endor", "Arrakis", "Geonosis", "Naboo"};
            Locations = locations.Select(x => new SelectListItem { Text = x, Value = x }).ToList();
        }

        public  IActionResult OnPost()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    NewFacility.Name = FacilityView.Name;
                    NewFacility.Location = FacilityView.Location;
                    facilityService.Add(NewFacility);
                    _logger.LogInformation("New Facility Created");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating facility" + ex);
            }
            return LocalRedirect("/Facilities");
        }
    }
}

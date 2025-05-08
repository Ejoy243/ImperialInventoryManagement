using ImperialInventoryManagement.Models;
using ImperialInventoryManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ImperialInventoryManagement.Pages
{
    public class FacilitiesModel : PageModel
    {
        private readonly ILogger<FacilitiesModel> _logger;
        private readonly FacilityService _facilityService;
        public List<Facility> Facilities { get; set; }

        public FacilitiesModel(ILogger<FacilitiesModel> logger, FacilityService fservice)
        {
            _logger = logger;
            _facilityService = fservice;
            Facilities = new List<Facility>();
        }
        public void OnGet()
        {
            try
            {
                Facilities = _facilityService.GetFacilities();
               
            }
            catch(Exception ex)
            {
                _logger.LogError("Error loading facilities " + ex);
            }
        }
    }
}

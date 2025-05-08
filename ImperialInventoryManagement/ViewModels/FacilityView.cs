using System.ComponentModel.DataAnnotations;

namespace ImperialInventoryManagement.ViewModels
{
    public class FacilityView
    {
        [StringLength(512)]
        public String Name { get; set; }
        public String Location { get; set; }
    }
}

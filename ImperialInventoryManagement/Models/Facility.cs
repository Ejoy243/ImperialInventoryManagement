using System.ComponentModel.DataAnnotations;

namespace ImperialInventoryManagement.Models
{
    public class Facility : EntityBase
    {
        public List<InventoryItem> InventoryItems { get; set; }
        public string Location { get; set; }
        [StringLength(512)]
        public string Name { get; set; }


        public Facility() 
        {
            InventoryItems = new List<InventoryItem>();
            Location = string.Empty;
            Name = string.Empty;
        }


    }
    
}

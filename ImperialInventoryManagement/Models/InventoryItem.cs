using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImperialInventoryManagement.Models
{
    public class InventoryItem : EntityBase
    {
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int FacilityId { get; set; }
        public Facility Facility {get; set;}

        public int ItemAmount { get; set; }
        public int ItemReserve { get; set; }

        public InventoryItem()
        {
            //Item = new Item();
        } 
            
    }

    
}

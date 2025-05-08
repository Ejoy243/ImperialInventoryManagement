using System.ComponentModel.DataAnnotations.Schema;

namespace ImperialInventoryManagement.Models
{
    public class Order : EntityBase
    {
        public string Purpose { get; set; }
        public DateTime DueDate { get; set; }
        
        public int InventoryItemId { get; set; }

        public InventoryItem InventoryItem { get; set; }
        public Order() 
        {
            Purpose = string.Empty;
            DueDate = DateTime.MinValue;
        }
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace ImperialInventoryManagement.Models
{
    public class ItemCategory : EntityBase
    {

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int ItemId {  get; set; }
        public Item Item { get; set; }

        public ItemCategory() 
        {
         
        }
    }
}

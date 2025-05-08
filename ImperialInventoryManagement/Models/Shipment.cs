using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImperialInventoryManagement.Models
{
    public class Shipment : EntityBase
    {
        public IdentityUser CreatedBy { get; set; }
        [ForeignKey("IdentityUser")]
        public string UserId { get; set; }

        public string Status { get; set; } 
        public string TrackingID { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public Shipment() 
        {
            CreatedBy = new IdentityUser();
            UserId = string.Empty;
            Status = string.Empty;
            TrackingID = string.Empty;
        }

    }
}

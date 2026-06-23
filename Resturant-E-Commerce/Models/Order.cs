using System.ComponentModel.DataAnnotations.Schema;

namespace Resturant_E_Commerce.Models
{
    public class Order
    {
        
        public int             OrderId { get; set; }
        public DateTime        OrderDate { get; set; }
        public string?         UserId { get; set; }
        public ApplicationUser User { get; set; }
        public decimal         TotalAmount { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }

    }
}

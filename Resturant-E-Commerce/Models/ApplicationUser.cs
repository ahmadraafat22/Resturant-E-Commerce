using Microsoft.AspNetCore.Identity;

namespace Resturant_E_Commerce.Models
{
    public class ApplicationUser:IdentityUser
    {
        public ICollection<Order>? Orders { get; set; }
    }
}

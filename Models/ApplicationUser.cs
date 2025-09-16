using Microsoft.AspNetCore.Identity;
using MutluSepet.Data;    // DbContext
using MutluSepet.Models;  // Model sınıfları

namespace MutluSepet.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;  // null uyarısını önler
        public List<Favorite> Favorites { get; set; } = new List<Favorite>();  // null uyarısını önler
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();  // null uyarısını önler
    }
}

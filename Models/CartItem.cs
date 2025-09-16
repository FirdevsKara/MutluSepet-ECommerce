using MutluSepet.Data;    // DbContext
using MutluSepet.Models;  // Model sınıfları

namespace MutluSepet.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;  // null uyarısını önler
        public string UserId { get; set; } = string.Empty;  // null uyarısını önler
        public ApplicationUser User { get; set; } = null!;  // null uyarısını önler
        public int Quantity { get; set; }
    }
}

using MutluSepet.Data;    // DbContext
using MutluSepet.Models;  // Model sınıfları

namespace MutluSepet.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;        // varsayılan değer
        public ApplicationUser User { get; set; } = null!;       // EF Core için null değil ama başlangıçta boş değil
        public List<CartItem> Items { get; set; } = new();       // boş liste ile başlat
        public DateTime CreatedAt { get; set; } = DateTime.Now;  // varsayılan olarak oluşturulma zamanı
    }
}

using MutluSepet.Data;    // DbContext
using MutluSepet.Models;  // Model sınıfları

namespace MutluSepet.Models
{
    public class Favorite
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;    // EF Core için null değil ama başlangıçta boş
        public string UserId { get; set; } = string.Empty; 
        public ApplicationUser User { get; set; } = null!;
    }
}

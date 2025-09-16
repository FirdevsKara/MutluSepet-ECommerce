using MutluSepet.Data;    // DbContext
using System.ComponentModel.DataAnnotations.Schema;

namespace MutluSepet.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;           // varsayılan değer
        public string Description { get; set; } = string.Empty;    // varsayılan değer

        [Column(TypeName = "decimal(18,2)")] 
        public decimal Price { get; set; }
        
        public string ImageUrl { get; set; } = string.Empty;       // varsayılan değer
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;           // EF Core için null değil ama başlangıçta boş değil
        public List<Comment> Comments { get; set; } = new();      // boş liste ile başlat
    }
}

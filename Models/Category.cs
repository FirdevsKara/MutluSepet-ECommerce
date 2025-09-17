using MutluSepet.Models;
namespace MutluSepet.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // null uyarısını önler
        public List<Product> Products { get; set; } = new List<Product>(); // boş liste ile başlat
    }
}

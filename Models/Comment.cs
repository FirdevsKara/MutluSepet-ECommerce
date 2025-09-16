namespace MutluSepet.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;  // null uyarısını önler
        public int Rating { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;     // EF Core ilişkisi için
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;
    }
}

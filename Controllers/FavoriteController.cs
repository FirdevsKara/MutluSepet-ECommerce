using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MutluSepet.Models;  // Modeller burada
using System.Linq;
using System.Security.Claims;
using MutluSepet.Data;

namespace MutluSepet.Controllers
{
    [Authorize] // Giriş yapmış kullanıcılar erişebilir
    public class FavoriteController : Controller
    {
        private readonly ApplicationDbContext _context;
        public FavoriteController(ApplicationDbContext context) { _context = context; }

        // Favorilerim sayfası
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var favs = _context.Favorites
                               .Include(f => f.Product) // Ürün bilgilerini getir
                               .Where(f => f.UserId == userId)
                               .ToList();
            return View(favs);
        }

        // Ürünü favorilere ekle
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToFavorite(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!_context.Favorites.Any(f => f.ProductId == productId && f.UserId == userId))
            {
                _context.Favorites.Add(new Favorite { ProductId = productId, UserId = userId });
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // Favoriden ürünü kaldır
        [HttpPost]
        public IActionResult Remove(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var fav = _context.Favorites.FirstOrDefault(f => f.Id == id && f.UserId == userId);

            if (fav != null)
            {
                _context.Favorites.Remove(fav);
                _context.SaveChanges();
            }

            return RedirectToAction("Index"); // Sayfayı yenileyip güncel listeyi göster
        }
    }
}

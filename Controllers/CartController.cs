using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using MutluSepet.Data;
using MutluSepet.Models;

namespace MutluSepet.Controllers
{
    [Authorize] // Giriş yapmayan kullanıcı bu controllera erişemez
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CartController(ApplicationDbContext context) { _context = context; }

        // Sepetim sayfası
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = _context.CartItems
                .Include(c => c.Product) // Ürün bilgilerini getir
                .Where(c => c.UserId == userId)
                .ToList();

            return View(cart);
        }

        // Sepete ürün ekleme
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var item = _context.CartItems
                .FirstOrDefault(c => c.ProductId == productId && c.UserId == userId);

            if (item != null)
            {
                item.Quantity++; // aynı üründen varsa miktarı artır
            }
            else
            {
                _context.CartItems.Add(new CartItem
                {
                    ProductId = productId,
                    UserId = userId,
                    Quantity = 1
                });
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Favorite");
        }

        // Sepetten ürün kaldırma
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var item = _context.CartItems.FirstOrDefault(c => c.Id == id && c.UserId == userId);

            if (item != null)
            {
                _context.CartItems.Remove(item);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}

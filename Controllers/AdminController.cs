using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MutluSepet.Data;
using MutluSepet.Models;

namespace MutluSepet.Controllers
{
    // 🔐 Bu controller sadece Admin rolündeki kullanıcılar tarafından erişilebilir
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor ile DbContext inject
        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 🟢 Ürünleri listeleme sayfası
        public IActionResult Products()
        {
            var products = _context.Products
                .Include(p => p.Category) // Ürünün kategorisini de çek
                .ToList();

            ViewData["Title"] = "Ürünler";
            return View(products); // Views/Admin/Products.cshtml kullanılacak
        }

        // ➕ Yeni ürün ekleme (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
            }
            return RedirectToAction("Products");
        }

        // ❌ Ürün silme
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            return RedirectToAction("Products");
        }

        // 🗂️ Kategorileri listeleme
        public IActionResult Categories()
        {
            var categories = _context.Categories.ToList();
            ViewData["Title"] = "Kategoriler";
            return View(categories); // Views/Admin/Categories.cshtml
        }

        // 🛒 Siparişleri listeleme
        public IActionResult Orders()
        {
            var orders = _context.Orders
                .Include(o => o.User) // Siparişi veren kullanıcı
                .Include(o => o.Items)
                    .ThenInclude(i => i.Product) // Sepetteki ürünler
                .ToList();

            ViewData["Title"] = "Siparişler";
            return View(orders); // Views/Admin/Orders.cshtml
        }
    }
}

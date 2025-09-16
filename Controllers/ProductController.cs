using MutluSepet.Data;
using MutluSepet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MutluSepet.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context) { _context = context; }

        public IActionResult Index(int? categoryId, string search)
        {
            var products = _context.Products.Include(p => p.Category).AsQueryable();
            if (categoryId != null) products = products.Where(p => p.CategoryId == categoryId);
            if (!string.IsNullOrEmpty(search)) products = products.Where(p => p.Name.Contains(search));
            return View(products.ToList());
        }

        public IActionResult Details(int id)
        {
            var product = _context.Products.Include(p => p.Comments).FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }

        public IActionResult Category(int id)
        {
            // Kategorinin adı
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
                return NotFound();

            // O kategoriye ait ürünler
            var products = _context.Products
                            .Where(p => p.CategoryId == id)
                            .ToList();

            // ViewData ile başlığı gönder
            ViewData["Title"] = $" ({category.Name})";

            return View("Index", products); // Index view'ını kullanabiliriz
        }

        // 🔎 Arama tahminleri için yeni action
        [HttpGet]
public IActionResult SearchSuggestions(string term)
{
    if (string.IsNullOrWhiteSpace(term))
        return Json(new List<string>());

    var suggestions = _context.Products
        .Where(p => p.Name.Contains(term))
        .Select(p => p.Name)
        .Distinct()
        .Take(5) // max 5 öneri
        .ToList();

    return Json(suggestions);
}

    }
}

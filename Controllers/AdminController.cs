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
        // public IActionResult Products()
        // {
        //     var products = _context.Products
        //         .Include(p => p.Category) // Ürünün kategorisini de çek
        //         .ToList();

        //     ViewData["Title"] = "Ürünler";
        //     return View(products); // Views/Admin/Products.cshtml kullanılacak
        // }

        public IActionResult Products()
        {
            var products = _context.Products
                .Include(p => p.Category) // Ürünün kategorisini de çek
                .ToList();

            ViewData["Title"] = "Ürünler";

            // Kategorileri ViewBag ile view'e gönder
            ViewBag.Categories = _context.Categories.ToList();

            return View(products); // Views/Admin/Products.cshtml kullanılacak
        }
        // // ➕ Yeni ürün ekleme (POST)

        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public IActionResult AddProduct([Bind("Name,Description,Price,Stock,CategoryId,ImageUrl")]Product product)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         // Eğer validation hatası varsa tekrar aynı view'i dönelim
        //         ViewBag.Categories = _context.Categories.ToList();
        //         var products = _context.Products.Include(p => p.Category).ToList();
        //         return View("Products", products);
        //     }

        //     _context.Products.Add(product);
        //     _context.SaveChanges();

        //     return RedirectToAction("Products");
        // }
        

[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult AddProduct(Product product)
{
    if (!ModelState.IsValid)
    {
        var errors = ModelState
            .SelectMany(x => x.Value.Errors.Select(e => new { x.Key, e.ErrorMessage }))
            .ToList();

        return Json(errors); // Hangi alan hatalı görebileceğiz
    }

    _context.Products.Add(product);
    _context.SaveChanges();
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
// ➕ Yeni kategori ekleme (POST)
[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult AddCategory(Category category)
{
    if (ModelState.IsValid)
    {
        _context.Categories.Add(category);
        _context.SaveChanges();
    }
    return RedirectToAction("Categories");
}

// ❌ Kategori silme
public IActionResult DeleteCategory(int id)
{
    var category = _context.Categories.Find(id);
    if (category != null)
    {
        _context.Categories.Remove(category);
        _context.SaveChanges();
    }
    return RedirectToAction("Categories");
}





    }
}

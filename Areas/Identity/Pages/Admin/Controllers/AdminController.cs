using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MutluSepet.Data;
using Microsoft.EntityFrameworkCore;

namespace MutluSepet.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")] // Sadece admin erişebilir
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Dashboard
        public async Task<IActionResult> Dashboard()
        {
            ViewBag.TotalUsers = await _context.Users.CountAsync();
            ViewBag.TotalProducts = await _context.Products.CountAsync();
            ViewBag.TotalOrders = await _context.Orders.CountAsync();
            return View();
        }

        // Ürün listesi
        public async Task<IActionResult> Products()
        {
            var products = await _context.Products.Include(p => p.Category).ToListAsync();
            return View(products);
        }

        // Sipariş listesi
        public async Task<IActionResult> Orders()
        {
            var orders = await _context.Orders.Include(o => o.User).Include(o => o.Items).ThenInclude(i=>i.Product).ToListAsync();
            return View(orders);
        }

        // Kullanıcı listesi
        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using MutluSepet.Data;
using MutluSepet.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;


namespace MutluSepet.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CheckoutController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Checkout
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(string AddressLine, string City, string PostalCode)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Sepeti DB'den al
            var cart = await _context.CartItems
                                     .Include(c => c.Product)
                                     .Where(c => c.UserId == userId)
                                     .ToListAsync();

            if (cart == null || !cart.Any())
            {
                TempData["Error"] = "Sepetiniz boş!";
                return RedirectToAction("Index", "Cart");
            }

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                AddressLine = AddressLine,
                City = City,
                PostalCode = PostalCode,
                TotalAmount = cart.Sum(i => i.Quantity * i.Product.Price),
                OrderItems = cart.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    Price = i.Product.Price
                }).ToList()
            };

            _context.Orders.Add(order);

            // Sepeti DB'den temizle
            _context.CartItems.RemoveRange(cart);

            await _context.SaveChangesAsync();

            return RedirectToAction("OrderSuccess");
        }

        public IActionResult OrderSuccess()
        {
            return View();
        }
    }
}

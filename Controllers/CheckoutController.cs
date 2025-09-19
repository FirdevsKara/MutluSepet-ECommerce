using Microsoft.AspNetCore.Mvc;
using MutluSepet.Data;
using MutluSepet.Models;
using MutluSepet.Models.ViewModels; // <- ViewModel namespace
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
        public async Task<IActionResult> Checkout(CheckoutViewModel model)
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

            if (!ModelState.IsValid)
            {
                // Form hatalıysa tekrar göster
                return View(model);
            }

            // Siparişi oluştur
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                AddressLine = model.AddressLine,
                City = model.City,
                PostalCode = model.PostalCode,
                TotalAmount = cart.Sum(i => i.Quantity * i.Product.Price),
                OrderItems = cart.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    Price = i.Product.Price
                }).ToList()
            };

            _context.Orders.Add(order);

            // Sepeti temizle
            _context.CartItems.RemoveRange(cart);

            await _context.SaveChangesAsync();

            // Ödeme için kart bilgilerini burada kullanabilirsin
            // Örnek: model.CardNumber, model.ExpiryDate, model.CVV, model.CardName

            return RedirectToAction("OrderSuccess");
        }

        public IActionResult OrderSuccess()
        {
            return View();
        }
    }
}

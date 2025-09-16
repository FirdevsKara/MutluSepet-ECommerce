using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MutluSepet.Models;  // Modeller burada
using System.Linq;
using System.Security.Claims;
using MutluSepet.Data;

namespace MutluSepet.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CommentController(ApplicationDbContext context) { _context = context; }

        [HttpPost]
        public IActionResult Add(int productId, string text, int rating)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.Comments.Add(new Comment { ProductId = productId, UserId = userId, Text = text, Rating = rating });
            _context.SaveChanges();
            return RedirectToAction("Details", "Product", new { id = productId });
        }
    }
}
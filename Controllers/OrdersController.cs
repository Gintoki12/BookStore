using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.Data;
using BookStore.Models;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Orders/Create
        public IActionResult Create(int bookId)
        {
            var order = new Order
            {
                BookId = bookId,
                UserId = User.Identity.Name,
                Status = "Pending",
                OrderDate = DateTime.Now
            };
            return View(order);
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookId,UserId,Status,OrderDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Index
        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders.Include(o => o.Book).ToListAsync();
            return View(orders);
        }
    }
}

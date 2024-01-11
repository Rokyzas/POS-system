using Microsoft.AspNetCore.Mvc;
using POS.Data;

namespace POS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentTransactionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PaymentTransactionController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllTransactions()
        {
            var transactions = _context.paymentTransaction.ToList();

            return Ok(transactions);
        }

        // Add more endpoints for creating, updating, deleting payment transactions if needed
    }
}

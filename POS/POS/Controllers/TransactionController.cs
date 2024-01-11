using Microsoft.AspNetCore.Mvc;
using POS.Data;
using POS.Models;

namespace POS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransactionsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddTransaction(Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.transactions.Add(transaction);
            _context.SaveChanges();

            return CreatedAtAction(nameof(AddTransaction), new { id = transaction.Id }, transaction);
        }

        [HttpGet("{orderId}")]
        public IActionResult GetTransactionsByOrder(int orderId)
        {
            var transactions = _context.transactions
                .Where(t => t.OrderId == orderId)
                .ToList();

            if (transactions.Count == 0)
            {
                return NotFound(new { error = "No transactions found for the given order" });
            }

            return Ok(new { transactions });
        }

        [HttpGet]
        public IActionResult GetAllTransactions()
        {
            var allTransactions = _context.transactions.ToList();

            if (allTransactions.Count == 0)
            {
                return NotFound(new { error = "No transactions found" });
            }

            return Ok(new { transactions = allTransactions });
        }
    }
}

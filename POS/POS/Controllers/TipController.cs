using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POS.Data;
using POS.Models;

namespace POS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TipsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddTip(Tip tip)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.tip.Add(tip);
            _context.SaveChanges();

            return CreatedAtAction(nameof(AddTip), new { id = tip.Id }, tip);
        }

        [HttpGet("{orderId}")]
        public IActionResult GetTipsByOrder(int orderId)
        {
            var tips = _context.tip
                .Where(t => t.OrderId == orderId)
                .ToList();

            if (tips.Count == 0)
            {
                return NotFound(new { error = "No tips found for the given order" });
            }

            return Ok(new { tips });
        }

        [HttpGet]
        public IActionResult GetAllTips()
        {
            var allTips = _context.tip.ToList();

            if (allTips.Count == 0)
            {
                return NotFound(new { error = "No tips found" });
            }

            return Ok(new { tips = allTips });
        }
    }
}

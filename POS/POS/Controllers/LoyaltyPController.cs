using Microsoft.AspNetCore.Mvc;
using POS.Data;
using POS.Models;

namespace POS.Controllers
{

    [ApiController]
    [Route("api/loyaltyPoints")]


    public class LoyaltyPointsController: ControllerBase
    {

        private readonly AppDbContext _context;

        public LoyaltyPointsController(AppDbContext context)
        {
            _context = context;
        }

        // POST /api/loyaltyPoints - create new loyaltyPoints
        [HttpPost]
        public IActionResult CreateLoyaltyPoints([FromBody] LoyaltyPoints loyalty)
        {
            _context.loyaltyPoints.Add(loyalty);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetLoyaltyById), new { loyaltyId = loyalty.Id }, loyalty);
        }

        // GET /api/loyaltyPoints - Retrieve all loyalties
        [HttpGet]
        public IActionResult GetLoyaltys()
        {
            var loyalty = _context.loyaltyPoints.ToList();
            return Ok(loyalty);
        }

        // GET /api/loyaltyPoints/{loyaltyId} - retrieve specific loyalty by id
        [HttpGet("{loyaltyId}")]
        public IActionResult GetLoyaltyById(int loyaltyId)
        {
            var loyalty = _context.loyaltyPoints.Find(loyaltyId);
            if (loyalty == null)
            {
                return NotFound();
            }
            return Ok(loyalty);
        }

        // PUT /api/loyaltyPoints{loyaltyId} - update specific loyalty by id
        [HttpPut("{loyaltyId}")]
        public IActionResult UpdateLoyalty(int loyaltyId, [FromBody] LoyaltyPoints UpdatedLoyalty)
        {
            var existingLoyalty = _context.loyaltyPoints.Find(loyaltyId);
            if (existingLoyalty == null)
            {
                return NotFound();
            }

            existingLoyalty.CustomerId = UpdatedLoyalty.CustomerId;
            existingLoyalty.Points = UpdatedLoyalty.Points;


            _context.SaveChanges();
            return Ok(existingLoyalty);
        }

        //DELETE /api/loyalty{loyaltyId} - delete specific loyalty by id
        [HttpDelete("{loyaltyId}")]
        public IActionResult DeleteLoyalty(int loyaltyId)
        {
            var loyalty = _context.loyaltyPoints.Find(loyaltyId);
            if (loyalty == null)
            {
                return NotFound();
            }

            _context.loyaltyPoints.Remove(loyalty);
            _context.SaveChanges();
            return NoContent();
        }
    }
}

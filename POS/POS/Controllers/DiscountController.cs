using Microsoft.AspNetCore.Mvc;
using POS.Data;
using POS.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace POS.Controllers
{
    [ApiController]
    [Route("api/discount")]
    public class DiscountController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DiscountController(AppDbContext context)
        {
            _context = context;
        }

        // POST /api/discount - create new discount
        [HttpPost]
        public IActionResult CreateDiscount([FromBody] Discount discount)
        {
            _context.discount.Add(discount);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetDiscountById), new { discountId = discount.Id }, discount);
        }

        // GET /api/discount - Retrieve all discounts
        [HttpGet]
        public IActionResult GetDiscounts()
        {
            var discounts = _context.discount.ToList();
            return Ok(discounts);
        }

        // GET /api/discount/{discountId} - retrieve specific discount by id
        [HttpGet("{discountId}")]
        public IActionResult GetDiscountById(int discountId)
        {
            var discount = _context.discount.Find(discountId);
            if (discount == null)
            {
                return NotFound();
            }
            return Ok(discount);
        }

        // PUT /api/discount/{discountId} - update specific discount by id
        [HttpPut("{discountId}")]
        public IActionResult UpdateDiscount(int discountId, [FromBody] Discount updatedDiscount)
        {
            var existingDiscount = _context.discount.Find(discountId);
            if (existingDiscount == null)
            {
                return NotFound();
            }

            existingDiscount.Percentage = updatedDiscount.Percentage;
            existingDiscount.Description = updatedDiscount.Description;
            existingDiscount.ValidFrom = updatedDiscount.ValidFrom;
            existingDiscount.ValidUntil = updatedDiscount.ValidUntil;

            _context.SaveChanges();
            return Ok(existingDiscount);
        }

        // DELETE /api/discount/{discountId} - delete specific discount by id
        [HttpDelete("{discountId}")]
        public IActionResult DeleteDiscount(int discountId)
        {
            var discount = _context.discount.Find(discountId);
            if (discount == null)
            {
                return NotFound();
            }

            _context.discount.Remove(discount);
            _context.SaveChanges();
            return NoContent();
        }
    }
}

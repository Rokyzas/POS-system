using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POS.Data;
using POS.Models;
using System.Collections.Generic;
using System.Linq;

namespace POS.Controllers
{
    [ApiController]
    [Route("api/item")]
    public class ItemController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ItemController(AppDbContext context)
        {
            _context = context;
        }

        // POST /api/item - create new item
        [HttpPost]
        public IActionResult CreateItem([FromBody] Item item)
        {
            _context.item.Add(item);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetItemById), new { itemId = item.Id }, item);
        }

        // GET /api/item - Retrieve all items
        [HttpGet]
        public IActionResult GetItems()
        {
            var items = _context.item.ToList();
            return Ok(items);
        }

        // GET /api/item/{itemId} - retrieve specific item by id
        [HttpGet("{itemId}")]
        public IActionResult GetItemById(int itemId)
        {
            var item = _context.item.Find(itemId);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        // PUT /api/item/{itemId} - update specific item by id
        [HttpPut("{itemId}")]
        public IActionResult UpdateItem(int itemId, [FromBody] Item updatedItem)
        {
            var existingItem = _context.item.Find(itemId);
            if (existingItem == null)
            {
                return NotFound();
            }

            existingItem.Name = updatedItem.Name;
            existingItem.Price = updatedItem.Price;
            existingItem.DiscountId = updatedItem.DiscountId;
            existingItem.Tax = updatedItem.Tax;

            _context.SaveChanges();
            return Ok(existingItem);
        }

        // DELETE /api/item/{itemId} - delete specific item by id
        [HttpDelete("{itemId}")]
        public IActionResult DeleteItem(int itemId)
        {
            var item = _context.item.Find(itemId);
            if (item == null)
            {
                return NotFound();
            }

            _context.item.Remove(item);
            _context.SaveChanges();
            return NoContent();
        }
    }
}

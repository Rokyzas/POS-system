﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using POS.Data;
using POS.Models;

namespace POS.Controllers
{
    // orderItemsController.cs
    [ApiController]
    [Route("api/orderItems")]
    public class orderItemController : ControllerBase
    {
        private readonly AppDbContext _context;

        public orderItemController(AppDbContext context)
        {
            _context = context;
        }

        // POST /api/orderItems - Create a new orderItem
        [HttpPost]
        public IActionResult CreateorderItem([FromBody] OrderItem orderItem)
        {
            var order = _context.order.Find(orderItem.OrderId);
            if (order == null)
            {
                return NotFound("order not found");
            }

            orderItem.Order = order;
            var item = _context.item.Find(orderItem.ItemId);
            if (item == null)
            {
                return NotFound("item not found");
            }

            _context.orderItem.Add(orderItem);
            int lol = _context.SaveChanges();
            return CreatedAtAction(nameof(GetorderItemById), new { OrderId = orderItem.OrderId, ItemId = orderItem.ItemId }, orderItem);
        }
         
        // GET /api/orderItems - Retrieve all orderItems
        [HttpGet]
        public IActionResult GetorderItems()
        {
            var orderItems = _context.orderItem.ToList();
            return Ok(orderItems);
        }

        // GET /api/orderItems/{orderItemId} - Retrieve a specific orderItem by its ID
        [HttpGet("{orderId}/{itemId}")]
        public IActionResult GetorderItemById(int orderId, int itemId)
        {
            var orderItem = _context.orderItem.FirstOrDefault(e => e.OrderId == orderId && e.ItemId == itemId);
            if (orderItem == null)
            {
                return NotFound();
            }
            return Ok(orderItem);
        }

        // PUT /api/orderItems/{orderItemId} - Update a specific orderItem by its ID
        [HttpPut("{orderId}/{itemId}")]
        public IActionResult UpdateorderItem(int orderId, int itemId, [FromBody] OrderItem updatedorderItem)
        {
            var existingorderItem = _context.orderItem.FirstOrDefault(e => e.OrderId == orderId && e.ItemId == itemId);
            if (existingorderItem == null)
            {
                return NotFound();
            }

            existingorderItem.Amount = updatedorderItem.Amount;

            _context.SaveChanges();
            return Ok(existingorderItem);
        }

        // DELETE /api/orderItems/{orderItemId} - Delete a specific orderItem by its ID
        [HttpDelete("{orderId}/{itemId}")]
        public IActionResult DeleteorderItem(int orderId, int itemId)
        {
            var orderItem = _context.orderItem.FirstOrDefault(e => e.OrderId == orderId && e.ItemId == itemId);
            if (orderItem == null)
            {
                return NotFound();
            }

            _context.orderItem.Remove(orderItem);
            _context.SaveChanges();
            return NoContent();
        }
    }

}

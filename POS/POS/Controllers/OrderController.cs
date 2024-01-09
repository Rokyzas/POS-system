using Microsoft.AspNetCore.Mvc;
using POS.Data;
using POS.Models;

namespace POS.Controllers
{
    // ordersController.cs
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        // POST /api/orders - Create a new order
        [HttpPost]
        public IActionResult Createorder([FromBody] Order order)
        {
            _context.order.Add(order);
            int lol = _context.SaveChanges();
            return CreatedAtAction(nameof(GetorderById), new { OrderId = order.Id }, order);
        }
         
        // GET /api/orders - Retrieve all orders
        [HttpGet]
        public IActionResult Getorders()
        {
            var orders = _context.order.ToList();
            return Ok(orders);
        }

        // GET /api/orders/{orderId} - Retrieve a specific order by its ID
        [HttpGet("{orderId}")]
        public IActionResult GetorderById(int orderId)
        {
            var order = _context.order.Find(orderId);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        // PUT /api/orders/{orderId} - Update a specific order by its ID
        [HttpPut("{orderId}")]
        public IActionResult Updateorder(int orderId, [FromBody] Order updatedorder)
        {
            var existingorder = _context.order.Find(orderId);
            if (existingorder == null)
            {
                return NotFound();
            }

            existingorder.Price = updatedorder.Price;
            existingorder.CustomerID = updatedorder.CustomerID;
            existingorder.Customer = updatedorder.Customer;
            existingorder.StaffID = updatedorder.StaffID;
            existingorder.Staff = updatedorder.Staff;
            existingorder.Status = updatedorder.Status;

            _context.SaveChanges();
            return Ok(existingorder);
        }

        // DELETE /api/orders/{orderId} - Delete a specific order by its ID
        [HttpDelete("{orderId}")]
        public IActionResult Deleteorder(int orderId)
        {
            var order = _context.order.Find(orderId);
            if (order == null)
            {
                return NotFound();
            }

            _context.order.Remove(order);
            _context.SaveChanges();
            return NoContent();
        }
    }

}

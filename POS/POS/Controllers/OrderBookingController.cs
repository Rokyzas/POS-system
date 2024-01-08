using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using POS.Data;
using POS.Models;

namespace POS.Controllers
{
    // orderBookingsController.cs
    [ApiController]
    [Route("api/orderBookings")]
    public class orderBookingController : ControllerBase
    {
        private readonly AppDbContext _context;

        public orderBookingController(AppDbContext context)
        {
            _context = context;
        }

        // POST /api/orderBookings - Create a new orderBooking
        [HttpPost]
        public IActionResult CreateorderBooking([FromBody] OrderBooking orderBooking)
        {
            var order = _context.order.Find(orderBooking.orderId);
            if (order == null)
            {
                return NotFound("order not found");
            }

            orderBooking.order = order;
            var booking = _context.booking.Find(orderBooking.bookingId);
            if (booking == null)
            {
                return NotFound("booking not found");
            }

            orderBooking.booking = booking;
            _context.orderBooking.Add(orderBooking);
            int lol = _context.SaveChanges();
            return CreatedAtAction(nameof(GetorderBookingById), new { orderId = orderBooking.orderId, bookingId = orderBooking.bookingId }, orderBooking);
        }
         
        // GET /api/orderBookings - Retrieve all orderBookings
        [HttpGet]
        public IActionResult GetorderBookings()
        {
            var orderBookings = _context.orderBooking.ToList();
            return Ok(orderBookings);
        }

        // GET /api/orderBookings/{orderBookingId} - Retrieve a specific orderBooking by its ID
        [HttpGet("{orderId}/{bookingId}")]
        public IActionResult GetorderBookingById(int orderId, int bookingId)
        {
            var orderBooking = _context.orderBooking.FirstOrDefault(e => e.orderId == orderId && e.bookingId == bookingId);
            if (orderBooking == null)
            {
                return NotFound();
            }
            return Ok(orderBooking);
        }

        // DELETE /api/orderBookings/{orderBookingId} - Delete a specific orderBooking by its ID
        [HttpDelete("{orderId}/{bookingId}")]
        public IActionResult DeleteorderBooking(int orderId, int bookingId)
        {
            var orderBooking = _context.orderBooking.FirstOrDefault(e => e.orderId == orderId && e.bookingId == bookingId);
            if (orderBooking == null)
            {
                return NotFound();
            }

            _context.orderBooking.Remove(orderBooking);
            _context.SaveChanges();
            return NoContent();
        }
    }

}

using Microsoft.AspNetCore.Mvc;
using POS.Data;
using POS.Models;

namespace POS.Controllers
{
    [ApiController]
    [Route("api/booking")]
    public class BookingController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookingController(AppDbContext context)
        {
            _context = context;
        }

        // POST /api/booking - create new booking
        [HttpPost]
        public IActionResult CreateBooking([FromBody] Booking booking)
        {
            _context.booking.Add(booking);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetBookingById), new { Id = booking.Id }, booking);
        }

        // GET /api/booking - Retrieve all bookings
        [HttpGet]
        public IActionResult GetBookings()
        {
            var bookings = _context.booking.ToList();
            return Ok(bookings);
        }

        // GET /api/booking/{id} - retrieve a specific booking by ID
        [HttpGet("{id}")]
        public IActionResult GetBookingById(int id)
        {
            var booking = _context.booking.Find(id);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }

        // PUT /api/booking/{id} - update a specific booking by ID
        [HttpPut("{id}")]
        public IActionResult UpdateBooking(int id, [FromBody] Booking updatedBooking)
        {
            var existingBooking = _context.booking.Find(id);
            if (existingBooking == null)
            {
                return NotFound();
            }

            existingBooking.TimeSlotID = updatedBooking.TimeSlotID;
            existingBooking.ServiceID = updatedBooking.ServiceID;
            existingBooking.Status = updatedBooking.Status;

            _context.SaveChanges();
            return Ok(existingBooking);
        }

        // DELETE /api/booking/{id} - delete a specific booking by ID
        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(int id)
        {
            var booking = _context.booking.Find(id);
            if (booking == null)
            {
                return NotFound();
            }

            _context.booking.Remove(booking);
            _context.SaveChanges();
            return NoContent();
        }
    }
}



using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var order = _context.order.Find(orderBooking.OrderId);
            if (order == null)
            {
                return NotFound("order not found");
            }

            var booking = _context.booking.Find(orderBooking.BookingId);
            if (booking == null)
            {
                return NotFound("booking not found");
            }

            _context.orderBooking.Add(orderBooking);
            int lol = _context.SaveChanges();

            float price = CalculatePrice(orderBooking.BookingId);
            order.Price += price;
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetorderBookingById), new { OrderId = orderBooking.OrderId, BookingId = orderBooking.BookingId }, orderBooking);
        }

        float CalculatePrice(int bookingId)
        {
            float currentPrice = 0;
            var booking = _context.booking.Find(bookingId);
            if(booking != null)
            {
                var service = _context.service.Find(booking.ServiceID);
                if(service != null)
                {
                    float discountPercent = 1;

                    if (service.DiscountID != 0)
                    {
                        var discount = _context.discount.Find(service.DiscountID);

                        if (discount != null && discount.Percentage != 0)
                        {
                            discountPercent = 1 - ((float)discount.Percentage / 100);
                        }
                    }
                    float tax = 1;
                    if (service.Tax != 0)
                    {
                        tax = service.Tax;
                    }

                    currentPrice = (float)service.Price * discountPercent;
                    float currentTax = currentPrice * tax;
                    currentPrice += currentTax;
                }
            }
            return currentPrice;
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
            var orderBooking = _context.orderBooking.FirstOrDefault(e => e.OrderId == orderId && e.BookingId == bookingId);
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
            var orderBooking = _context.orderBooking.FirstOrDefault(e => e.OrderId == orderId && e.BookingId == bookingId);
            if (orderBooking == null)
            {
                return NotFound();
            }

            float price = CalculatePrice(bookingId);

            RemoveFromOrder(orderId, price);

            _context.orderBooking.Remove(orderBooking);
            _context.SaveChanges();
            return NoContent();
        }

        void RemoveFromOrder(int orderId, float price)
        {
            var order = _context.order.Find(orderId);
            if (order != null)
            {
                order.Price -= price;
                _context.SaveChanges();
            }
        }
    }

}

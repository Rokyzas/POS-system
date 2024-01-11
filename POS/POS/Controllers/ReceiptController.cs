using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POS.Data;
using POS.Models;

namespace POS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReceiptController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReceiptController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GenerateReceipt(int orderId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Order order = _context.order.Where(t => t.Id == orderId).FirstOrDefault();

            // Get all order items associated with the order
            var orderItems = _context.orderItem
                .Where(oi => oi.OrderId == order.Id)
                .ToList();

            var orderBookings = _context.orderBooking
                .Where(oi => oi.OrderId == order.Id)
                .ToList();

            // Calculate the total price of items with discounts applied
            float orderTotal = 0;

            // Prepare a list to store item details
            var itemDetails = new List<object>();
            var bookingDetails = new List<object>();

            foreach (var orderItem in orderItems)
            {
                var item = _context.item
                            .Where(i => i.Id == orderItem.ItemId)
                            .Select(i => new
                            {
                                Id = i.Id,
                                Name = i.Name,
                                Price = i.Price,
                                Tax = i.Tax,
                                DiscountId = i.DiscountId,
                                Discount = _context.discount.FirstOrDefault(d => d.Id == i.DiscountId)
                            })
                            .FirstOrDefault();


                float itemTotal = orderItem.Amount * item.Price;

                float itemDiscount = itemTotal * item.Discount.Percentage / 100;

                float itemTax = (itemTotal - itemDiscount) * item.Tax;


                // Add item details to the list
                itemDetails.Add(new
                {
                    ItemName = item?.Name,
                    Quantity = orderItem.Amount,
                    ItemPrice = item.Price,
                    ItemTotal = itemTotal,
                    ItemDiscount = itemDiscount,
                    ItemTax = itemTax,
                    Total = itemTotal - itemDiscount + itemTax
                }); ;

                orderTotal += itemTotal - itemDiscount + itemTax;
            }

            foreach (var orderBooking in orderBookings)
            {
                var booking = _context.booking.Where(i => i.id == orderBooking.BookingId).FirstOrDefault();
                var item = _context.service
                            .Where(i => i.id == booking.ServiceID)
                            .Select(i => new
                            {
                                Id = i.id,
                                Name = i.Name,
                                Price = i.Price,
                                Tax = i.Tax,
                                DiscountId = i.DiscountID,
                                Discount = _context.discount.FirstOrDefault(d => d.Id == i.DiscountID)
                            })
                            .FirstOrDefault();


                float itemTotal = item.Price;

                float itemDiscount = itemTotal * item.Discount.Percentage / 100;

                float itemTax = (itemTotal - itemDiscount) * item.Tax;


                // Add item details to the list
                bookingDetails.Add(new
                {
                    ItemName = item?.Name,
                    Quantity = 1,
                    ItemPrice = item.Price,
                    ItemTotal = itemTotal,
                    ItemDiscount = itemDiscount,
                    ItemTax = itemTax,
                    Total = itemTotal - itemDiscount + itemTax
                }); ;

                orderTotal += itemTotal - itemDiscount + itemTax;
            }

            Tip tip = (Tip)_context.tip.Where(i => i.OrderId == order.Id).FirstOrDefault();
            float tipAmount; 

            if (tip == null)
            {
                tipAmount = 0;
            }
            else
            {
                tipAmount = tip.Amount;
            }

            // Create a detailed receipt
            var receipt = new
            {
                OrderId = order.Id,
                OrderStatus = order.Status,
                Items = itemDetails,
                Services = bookingDetails,
                TipAmount = tipAmount,
                Total = orderTotal + tipAmount
            };

            // Return the detailed receipt
            return Ok(receipt);
        }
    }   
}


﻿using POS.Models;

namespace POS.Data
{
    public class PaymentService
    {
        private readonly AppDbContext _context;

        public PaymentService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<CardPaymentResponse> ProcessCardPayment(int orderId)
        {
            // Mocking a card payment gateway API (replace with a real implementation)
            // In a real scenario, you would call the actual payment gateway API
            // For demonstration purposes, just returning a success response
            return new CardPaymentResponse { Success = true, Message = "Card payment successful" };
        }

        public CashPaymentResponse ProcessCashPayment(int orderId, float cashTendered=0)
        {
            Order order = _context.order.Where(t => t.Id == orderId).FirstOrDefault();
            if (order == null)
            {
                return new CashPaymentResponse { Success = false, Message = "Invalid order ID" };
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

            float amount = order.Price + tipAmount;

            if (cashTendered < amount)
            {
                return new CashPaymentResponse { Success = false, Message = "Insufficient cash provided" };
            }

            float change = cashTendered - amount;
            return new CashPaymentResponse { Success = true, Message = "Cash payment successful", Change = change };
        }
    }
}

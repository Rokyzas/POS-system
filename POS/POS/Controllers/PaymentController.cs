using Microsoft.AspNetCore.Mvc;
using POS.Data;
using POS.Models;

namespace POS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;

        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("processPayment")]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequest paymentRequest)
        {
            try
            {
                if (paymentRequest.PaymentMethod == PaymentMethod.Card)
                {
                    var paymentResponse = await _paymentService.ProcessCardPayment(paymentRequest.OrderId);
                    return Ok(paymentResponse);
                }
                else if (paymentRequest.PaymentMethod == PaymentMethod.Cash)
                {
                    var cashPaymentResponse = _paymentService.ProcessCashPayment(paymentRequest.OrderId, paymentRequest.CashTendered);
                    return Ok(cashPaymentResponse);
                }
                else
                {
                    return BadRequest("Invalid payment method");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error processing payment: {ex.Message}");
            }
        }
    }
}

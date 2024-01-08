using Microsoft.AspNetCore.Mvc;
using POS.Data;
using POS.Models;

namespace POS.Controllers
{
    [ApiController]
    [Route("api/customer")]

    public class CustomerController: ControllerBase 
    {
        private readonly AppDbContext _context;

        public CustomerController(AppDbContext context)
        {
            _context = context;
        }

        // POST /api/customer - create new customer
        [HttpPost]
        public IActionResult CreateCustomer([FromBody] Customer customer)
        {
            _context.customer.Add(customer);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetCustomerById), new { customerId = customer.Id }, customer);
        }

        // GET /api/customer - Retrieve all customers
        [HttpGet]
        public IActionResult GetCustomers()
        {
            var customer = _context.customer.ToList();
            return Ok(customer);
        }

        // GET /api/customer/{customerId} - retrieve specific customer by id
        [HttpGet("{customerId}")]
        public IActionResult GetCustomerById(int customerId)
        {
            var customer = _context.customer.Find(customerId);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        // PUT /api/customer{customerId} - update specific customer by id
        [HttpPut("{customerId}")]
        public IActionResult UpdateCustomer(int customerId, [FromBody] Customer UpdatedCustomer)
        {
            var existingCustomer = _context.customer.Find(customerId);
            if (existingCustomer == null)
            {
                return NotFound();
            }

            existingCustomer.Name = UpdatedCustomer.Name;
            existingCustomer.Email = UpdatedCustomer.Email;
            existingCustomer.Phone = UpdatedCustomer.Phone;


            _context.SaveChanges();
            return Ok(existingCustomer);
        }

        //DELETE /api/customer{customerId} - delete specific customer by id
        [HttpDelete("{customerId}")]
        public IActionResult DeleteCustomer(int customerId)
        {
            var customer = _context.customer.Find(customerId);
            if (customer == null)
            {
                return NotFound();
            }

            _context.customer.Remove(customer);
            _context.SaveChanges();
            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.Data;
using POS.Models;

namespace POS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {

        private readonly AppDbContext _context;

        public ServiceController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/service - Create a new service
        [HttpPost]
        public IActionResult CreateService([FromBody] Service service)
        {
            _context.service.Add(service);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetServiceById), new { serviceId = service.id }, service);
        }

        // GET: api/service - Retrieve all services
        [HttpGet]
        public IActionResult GetServices()
        {
            var services = _context.service.ToList();
            return Ok(services);
        }

        // GET: api/service/{serviceId} - Retrieve a specific service by id
        [HttpGet("{serviceId}")]
        public IActionResult GetServiceById(int serviceId)
        {
            var service = _context.service.Find(serviceId);
            if (service == null)
            {
                return NotFound();
            }
            return Ok(service);
        }

        // PUT: api/service/{serviceId} - Update a specific service by id
        [HttpPut("{serviceId}")]
        public IActionResult UpdateService(int serviceId, [FromBody] Service updatedService)
        {
            var existingService = _context.service.Find(serviceId);
            if (existingService == null)
            {
                return NotFound();
            }

            existingService.Price = updatedService.Price;
            existingService.Name = updatedService.Name;
            existingService.Description = updatedService.Description;
            existingService.DiscountID = updatedService.DiscountID;

            _context.SaveChanges();
            return Ok(existingService);
        }

        // DELETE: api/service/{serviceId} - Delete a specific service by id
        [HttpDelete("{serviceId}")]
        public IActionResult DeleteService(int serviceId)
        {
            var service = _context.service.Find(serviceId);
            if (service == null)
            {
                return NotFound();
            }

            _context.service.Remove(service);
            _context.SaveChanges();
            return NoContent();
        }
    }
}



using Microsoft.AspNetCore.Mvc;
using POS.Data;
using POS.Models;

namespace POS.Controllers
{
    [ApiController]
    [Route("api/timeslot")]
    public class TimeSlotController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TimeSlotController(AppDbContext context)
        {
            _context = context;
        }

        // POST /api/timeslot - create new timeslot
        [HttpPost]
        public IActionResult CreateTimeSlot([FromBody] TimeSlot timeSlot)
        {
            _context.timeSlot.Add(timeSlot);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetTimeSlotById), new { id = timeSlot.id }, timeSlot);
        }

        // GET /api/timeslot - Retrieve all timeslots
        [HttpGet]
        public IActionResult GetTimeSlots()
        {
            var timeSlots = _context.timeSlot.ToList();
            return Ok(timeSlots);
        }

        // GET /api/timeslot/{id} - retrieve specific timeslot by id
        [HttpGet("{id}")]
        public IActionResult GetTimeSlotById(int id)
        {
            var timeSlot = _context.timeSlot.Find(id);
            if (timeSlot == null)
            {
                return NotFound();
            }
            return Ok(timeSlot);
        }

        // PUT /api/timeslot/{id} - update specific timeslot by id
        [HttpPut("{id}")]
        public IActionResult UpdateTimeSlot(int id, [FromBody] TimeSlot updatedTimeSlot)
        {
            var existingTimeSlot = _context.timeSlot.Find(id);
            if (existingTimeSlot == null)
            {
                return NotFound();
            }

            existingTimeSlot.StartDate = updatedTimeSlot.StartDate;
            existingTimeSlot.EndDate = updatedTimeSlot.EndDate;
            existingTimeSlot.StaffID = updatedTimeSlot.StaffID;
            existingTimeSlot.Status = updatedTimeSlot.Status;

            _context.SaveChanges();
            return Ok(existingTimeSlot);
        }

        // DELETE /api/timeslot/{id} - delete specific timeslot by id
        [HttpDelete("{id}")]
        public IActionResult DeleteTimeSlot(int id)
        {
            var timeSlot = _context.timeSlot.Find(id);
            if (timeSlot == null)
            {
                return NotFound();
            }

            _context.timeSlot.Remove(timeSlot);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
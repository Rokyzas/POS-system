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
        private readonly TimeSlotService _timeService;

        public TimeSlotController(AppDbContext context, TimeSlotService timeService)
        {
            _context = context;
            _timeService = timeService;
        }

        // POST /api/timeslot - create new timeslot
        [HttpPost]
        public IActionResult CreateTimeSlot([FromBody] TimeSlot timeSlot)
        {
            var timeslots = _context.timeSlot;
            var list = _timeService.FilterByStaffAndDay(timeslots, timeSlot.StartDate, timeSlot.StaffID);
            var intervals = _timeService.FindAvailableSlots(list, timeSlot.StartDate);
            if (_timeService.CheckIfWithinTimeInterval(timeSlot, intervals))
            {
                _context.timeSlot.Add(timeSlot);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetTimeSlotById), new { id = timeSlot.id }, timeSlot);
            }
            else
            {
                return Conflict("Time has already been booked");
            }
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

        // GET /api/timeSlot/{timeSlotId} - Retrieve a time available timeslots for a staff member on a given day
        [HttpGet("GetAvailableSlotsByStaffIDandDate")]
        public IActionResult GetAvailableTimeSlot(int staffId, DateTime time)
        {
            var timeslots = _context.timeSlot;
            var list = _timeService.FilterByStaffAndDay(timeslots, time, staffId);
            var intervals = _timeService.FindAvailableSlots(list, time);

            if (intervals == null)
            {
                return NotFound();
            }
            return Ok(intervals);
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
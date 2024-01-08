using Microsoft.AspNetCore.Mvc;
using POS.Data;
using POS.Models;

namespace POS.Controllers
{
    [ApiController]
    [Route("api/staff")]
    public class StaffController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StaffController(AppDbContext context)
        {
            _context = context;
        }

        // POST /api/staff - create new staff member
        [HttpPost]
        public IActionResult CreateStaff([FromBody] Staff staff)
        {
            _context.staff.Add(staff);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetStaffById), new { staffId = staff.StaffID }, staff);
        }

        // GET /api/staff - Retrieve all staff members
        [HttpGet]
        public IActionResult GetStaffMembers()
        {
            var staffMembers = _context.staff.ToList();
            return Ok(staffMembers);
        }

        // GET /api/staff/{staffId} - retrieve a specific staff member by ID
        [HttpGet("{staffId}")]
        public IActionResult GetStaffById(int staffId)
        {
            var staff = _context.staff.Find(staffId);
            if (staff == null)
            {
                return NotFound();
            }
            return Ok(staff);
        }

        // PUT /api/staff/{staffId} - update a specific staff member by ID
        [HttpPut("{staffId}")]
        public IActionResult UpdateStaff(int staffId, [FromBody] Staff updatedStaff)
        {
            var existingStaff = _context.staff.Find(staffId);
            if (existingStaff == null)
            {
                return NotFound();
            }

            existingStaff.Name = updatedStaff.Name;
            existingStaff.StartDate = updatedStaff.StartDate;
            existingStaff.RoleID = updatedStaff.RoleID;

            _context.SaveChanges();
            return Ok(existingStaff);
        }

        // DELETE /api/staff/{staffId} - delete a specific staff member by ID
        [HttpDelete("{staffId}")]
        public IActionResult DeleteStaff(int staffId)
        {
            var staff = _context.staff.Find(staffId);
            if (staff == null)
            {
                return NotFound();
            }

            _context.staff.Remove(staff);
            _context.SaveChanges();
            return NoContent();
        }
    }
}

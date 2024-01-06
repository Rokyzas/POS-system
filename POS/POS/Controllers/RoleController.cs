using Microsoft.AspNetCore.Mvc;
using POS.Data;
using POS.Models;

namespace POS.Controllers
{
    // RolesController.cs
    [ApiController]
    [Route("api/roles")]
    public class RoleController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RoleController(AppDbContext context)
        {
            _context = context;
        }

        // POST /api/roles - Create a new role
        [HttpPost]
        public IActionResult CreateRole([FromBody] Role role)
        {
            _context.role.Add(role);
            int lol = _context.SaveChanges();
            return CreatedAtAction(nameof(GetRoleById), new { roleId = role.Id }, role);
        }
         
        // GET /api/roles - Retrieve all roles
        [HttpGet]
        public IActionResult GetRoles()
        {
            var roles = _context.role.ToList();
            return Ok(roles);
        }

        // GET /api/roles/{roleId} - Retrieve a specific role by its ID
        [HttpGet("{roleId}")]
        public IActionResult GetRoleById(int roleId)
        {
            var role = _context.role.Find(roleId);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        // PUT /api/roles/{roleId} - Update a specific role by its ID
        [HttpPut("{roleId}")]
        public IActionResult UpdateRole(int roleId, [FromBody] Role updatedRole)
        {
            var existingRole = _context.role.Find(roleId);
            if (existingRole == null)
            {
                return NotFound();
            }

            existingRole.Name = updatedRole.Name;

            _context.SaveChanges();
            return Ok(existingRole);
        }

        // DELETE /api/roles/{roleId} - Delete a specific role by its ID
        [HttpDelete("{roleId}")]
        public IActionResult DeleteRole(int roleId)
        {
            var role = _context.role.Find(roleId);
            if (role == null)
            {
                return NotFound();
            }

            _context.role.Remove(role);
            _context.SaveChanges();
            return NoContent();
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUDApi.Data;
using CRUDApi.Models;
using CRUDApi.DTO;

namespace CRUDApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly PeopleDbContext _context;

        public RolesController(PeopleDbContext context)
        {
            _context = context;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            var roles = _context.Roles
        .Select(r => new RoleDto
        {
            Id = r.Id,
            Description = r.Description,
        })
        .ToList();

            return Ok(roles);
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRole(int id)
        {
            var roles = _context.Roles
                    .Where(r => r.Id == id)
                    .Select(r => new RoleDTO
                    {
                        Id = r.Id,
                        Description = r.Description
                    })
                    .ToList();

            return (roles.Count != 0) ? Ok(roles) : NotFound();
        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(int id, Role role)
        {
            if (id != role.Id)
            {
                return BadRequest("El ID del rol no coincide.");
            }

            var existingRole = _context.Roles.FirstOrDefault(r => r.Id == id);
            if (existingRole == null)
            {
                return NotFound();
            }

            existingRole.Description = role.Description;

            _context.SaveChanges();

            return NoContent();
        }

        // POST: api/Roles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(RoleDTO roleDTO)
        {
            var role = new Role
            {
                Description = roleDTO.Description,
            };
            _context.Roles.Add(role);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetRole), new { id = role.Id }, role);
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = _context.Roles.FirstOrDefault(r => r.Id == id);
            if (role == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(role);
            _context.SaveChanges();

            return NoContent();
        }

        private bool RoleExists(int id)
        {
            return _context.Roles.Any(e => e.Id == id);
        }
    }
}

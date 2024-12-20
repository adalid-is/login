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
    public class UsersController : ControllerBase
    {
        private readonly PeopleDbContext _context;
        private readonly IPasswordService _passwordService;

        public UsersController(PeopleDbContext context, IPasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(CreateUserDTO createUserDto)
        {
            var user = new User
            {
                Username = createUserDto.UserName,
                Password = _passwordService.HashPassword(createUserDto.Password), // Encriptar la contraseña
                PeopleId = (int)createUserDto.PeopleId,
                Roles = await _context.Roles.Where(r => createUserDto.Roles.Select(role => role.Id).Contains(r.Id)).ToListAsync()
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var userDto = new UserDTO
            {
                Id = user.Id,
                UserName = user.Username,
                PeopleId = user.PeopleId,
                People = user.People != null ? new PeopleDTO
                {
                    Id = user.People.Id,
                    FirstName = user.People.FirstName
                    , LastName = user.People.LastName
                } : null,
                Roles = user.Roles.Select(r => new RoleDto
                {
                    Id = r.Id,
                    Description = r.Description
                }).ToList()
            };

            return CreatedAtAction("GetUser", new { id = user.Id }, userDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, EditUserDTO editUserDto)
        {
            if (id != editUserDto.Id)
            {
                return BadRequest();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.Username = editUserDto.UserName;
            user.PeopleId = (int)editUserDto.PeopleId;
            user.Roles = await _context.Roles.Where(r => editUserDto.Roles.Select(role => role.Id).Contains(r.Id)).ToListAsync();

            // Si la contraseña fue proporcionada, debe ser actualizada
            if (!string.IsNullOrEmpty(editUserDto.Password))
            {
                user.Password = _passwordService.HashPassword(editUserDto.Password); // Encriptar la nueva contraseña
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }

}


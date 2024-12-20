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
    public class PeoplesController : ControllerBase
    {
        private readonly PeopleDbContext _context;

        public PeoplesController(PeopleDbContext context)
        {
            _context = context;
        }

        // GET: api/People
        [HttpGet]
        public ActionResult<IEnumerable<People>> GetPeoples()
        {
            var peoples = _context.Peoples
                    .Include(p => p.City)  // Incluye la relación con City
                    .ThenInclude(c => c.Province)  // Incluye la relación con Province
                    .Select(p => new PeopleDTO
                    {
                        Id = p.Id,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        Age = p.Age,
                        Nationality = p.Nationality,
                        Address = p.Address,
                        PostalCode = p.PostalCode,
                        IdCity = p.City.Id,
                        City = new CityDTO
                        {
                            Id = p.City.Id,
                            Description = p.City.Description,
                            Province = new RoleDTO
                            {
                                Id = p.City.Province.Id,
                                Description = p.City.Province.Description
                            }
                        }
                    })
                    .ToList();

            return Ok(peoples);
        }

        // GET: api/People/5
        [HttpGet("{id}")]
        public ActionResult<People> GetPeople(int id)
        {
            var peoples = _context.Peoples
                    .Include(p => p.City)  // Incluye la relación con City
                    .ThenInclude(c => c.Province)  // Incluye la relación con Province
                    .Where(p => p.Id == id)
                    .Select(p => new PeopleDTO
                    {
                        Id = p.Id,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        Age = p.Age,
                        Nationality = p.Nationality,
                        Address = p.Address,
                        PostalCode = p.PostalCode,
                        IdCity = p.City.Id,
                        City = new CityDTO
                        {
                            Id = p.City.Id,
                            Description = p.City.Description,
                            Province = new RoleDTO
                            {
                                Id = p.City.Province.Id,
                                Description = p.City.Province.Description
                            }
                        }
                    })
                    .ToList();
            return (peoples.Count != 0) ? Ok(peoples) : NotFound();
        }

        // GET: api/People/city/5
        [HttpGet("city/{cityId}")]
        public ActionResult<IEnumerable<People>> GetPeoplesByCity(int cityId)
        {
            var peoples = _context.Peoples
                    .Include(p => p.City)  // Incluye la relación con City
                    .ThenInclude(c => c.Province)  // Incluye la relación con Province
                    .Where(p => p.IdCity == cityId)
                    .Select(p => new PeopleDTO
                    {
                        Id = p.Id,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        Age = p.Age,
                        Nationality = p.Nationality,
                        Address = p.Address,
                        PostalCode = p.PostalCode,
                        IdCity = p.City.Id,
                        City = new CityDTO
                        {
                            Id = p.City.Id,
                            Description = p.City.Description,
                            Province = new RoleDTO
                            {
                                Id = p.City.Province.Id,
                                Description = p.City.Province.Description
                            }
                        }
                    })
                    .ToList();

            return Ok(peoples);

        }

        // POST: api/People
        [HttpPost]
        public ActionResult<People> CreatePeople(CreatePeopleDTO peopleDTO)
        {
            // Verificar si la ciudad existe
            var city = _context.Cities.FirstOrDefault(c => c.Id == peopleDTO.IdCity);

            if (city == null)
            {
                return BadRequest("La ciudad especificada no existe.");
            }

            // Crear la nueva persona
            var people = new People
            {
                FirstName = peopleDTO.FirstName,
                LastName = peopleDTO.LastName,
                Age = peopleDTO.Age,
                Nationality = peopleDTO.Nationality,
                Address = peopleDTO.Address,
                PostalCode = peopleDTO.PostalCode,
                IdCity = peopleDTO.IdCity,  // Se asocia la ciudad por su Id
                City = city  // Se asigna el objeto City completo si es necesario
            };

            // Guardar la nueva persona
            _context.Peoples.Add(people);
            _context.SaveChanges();

            // Retornar la persona creada
            return CreatedAtAction(nameof(GetPeople), new { id = people.Id }, people);
        }

        // PUT: api/People/5
        [HttpPut("{id}")]
        public IActionResult UpdatePeople(int id, EditPeopleDTO peopleDTO)
        {
            if (id != peopleDTO.Id)
            {
                return BadRequest("El ID de la persona no coincide.");
            }

            var existingPeople = _context.Peoples.FirstOrDefault(p => p.Id == id);
            if (existingPeople == null)
            {
                return NotFound();
            }

            existingPeople.FirstName = peopleDTO.FirstName;
            existingPeople.LastName = peopleDTO.LastName;
            existingPeople.Age = peopleDTO.Age;
            existingPeople.Nationality = peopleDTO.Nationality;
            existingPeople.Address = peopleDTO.Address;
            existingPeople.PostalCode = peopleDTO.PostalCode;
            existingPeople.IdCity = peopleDTO.IdCity;

            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/People/5
        [HttpDelete("{id}")]
        public IActionResult DeletePeople(int id)
        {
            var persona = _context.Peoples.FirstOrDefault(p => p.Id == id);
            if (persona == null)
            {
                return NotFound();
            }

            _context.Peoples.Remove(persona);
            _context.SaveChanges();

            return NoContent();
        }
    }
}

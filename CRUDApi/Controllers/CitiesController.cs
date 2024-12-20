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
    public class CitiesController : ControllerBase
    {
        private readonly PeopleDbContext _context;

        public CitiesController(PeopleDbContext context)
        {
            _context = context;
        }

        // GET: api/City
        [HttpGet]
        public ActionResult<IEnumerable<City>> GetCities()
        {
            var cities = _context.Cities
                    .Include(c => c.Province)
                    .Select(c => new CityDTO
                    {
                        Id = c.Id,
                        Description = c.Description,
                        IdProvince = c.Province.Id,
                        Province = new RoleDTO
                        {
                            Id = c.Province.Id,
                            Description = c.Province.Description
                        }
                        })
                    .ToList();

            return Ok(cities);
        }

        // GET: api/City/5
        [HttpGet("{id}")]
        public ActionResult<City> GetCity(int id)
        {
            var cities = _context.Cities
                    .Where(c => c.Id == id)
                    .Select(c => new CityDTO
                    {
                        Id = c.Id,
                        Description = c.Description
                    })
                    .ToList();

            return (cities.Count != 0) ? Ok(cities) : NotFound();
        }

        // POST: api/City
        [HttpPost]
        public ActionResult<City> CreateCity(CityDTO cityDTO)
        {
            // Verifica que la provincia exista
            var city = new City
            {
                Description = cityDTO.Description,
            };
            _context.Cities.Add(city);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCity), new { id = city.Id }, city);
        }

        // PUT: api/City/5
        [HttpPut("{id}")]
        public IActionResult UpdateCity(int id, CityDTO city)
        {
            if (id != city.Id)
            {
                return BadRequest("El ID de la provincia no coincide.");
            }

            var existingCity = _context.Cities.FirstOrDefault(c => c.Id == id);
            if (existingCity == null)
            {
                return NotFound();
            }

            existingCity.Description = city.Description;

            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/City/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCity(int id)
        {
            var city = _context.Cities.FirstOrDefault(c => c.Id == id);
            if (city == null)
            {
                return NotFound();
            }

            _context.Cities.Remove(city);
            _context.SaveChanges();

            return NoContent();
        }
    }
}

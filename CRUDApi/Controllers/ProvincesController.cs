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
    public class ProvincesController : ControllerBase
    {
        private readonly PeopleDbContext _context;

        public ProvincesController(PeopleDbContext context)
        {
            _context = context;
        }

        // GET: api/Province
        [HttpGet]
        public ActionResult<IEnumerable<Province>> GetProvinces()
        {
            var provinces = _context.Provinces
                    .Select(p => new RoleDTO
                    {
                        Id = p.Id,
                        Description = p.Description
                    })
                    .ToList();

            return Ok(provinces);
        }

        // GET: api/Province/5
        [HttpGet("{id}")]
        public ActionResult<Province> GetProvince(int id)
        {
            var provinces = _context.Provinces
                    .Where(p => p.Id == id)
                    .Select(p => new RoleDTO
                    {
                        Id = p.Id,
                        Description = p.Description
                    })
                    .ToList();

            return (provinces.Count != 0) ? Ok(provinces) : NotFound();
        }

        // PUT: api/Province/5
        [HttpPut("{id}")]
        public IActionResult UpdateProvince(int id, RoleDTO province)
        {
            if (id != province.Id)
            {
                return BadRequest("El ID de la provincia no coincide.");
            }

            var existingProvince = _context.Provinces.FirstOrDefault(p => p.Id == id);
            if (existingProvince == null)
            {
                return NotFound();
            }

            existingProvince.Description = province.Description;

            _context.SaveChanges();

            return NoContent();
        }

        // POST: api/Province
        [HttpPost]
        public ActionResult<Province> CreateProvince(CreateProvinceDTO provinceDTO)
        {
            var province = new Province
            {
                Description = provinceDTO.Description,
            };
            _context.Provinces.Add(province);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetProvince), new { id = province.Id }, province);
        }

        // DELETE: api/Province/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProvince(int id)
        {
            var province = _context.Provinces.FirstOrDefault(p => p.Id == id);
            if (province == null)
            {
                return NotFound();
            }

            _context.Provinces.Remove(province);
            _context.SaveChanges();

            return NoContent();
        }
    }
}

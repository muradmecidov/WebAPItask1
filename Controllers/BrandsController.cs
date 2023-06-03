using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_API.DAL;
using WEB_API.Entities;
using WEB_API.Entities.Dtos.Cars;

namespace WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BrandsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var color = _context.Brands.Find(id);
            if (color == null)
            {
                return NotFound();
            }

            return Ok(color);
        }
        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            var result = await _context.Brands.ToListAsync();
            if (result.Count == 0) { return NotFound(); }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddCars(CreateBrandDto create)
        {
            Brand brand = new Brand
            {
                Name = create.Name,
            };
            await _context.Brands.AddAsync(brand);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateBrandDto update)
        {
            var result = await _context.Brands.FindAsync(update.Id);
            if (result is null)
            {
                return NotFound();
            }
            result.Name = update.Name;
            await _context.SaveChangesAsync();
            return NoContent();

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _context.Brands.FindAsync(id);
            if (result is null) { return NotFound(); }
            _context.Brands.Remove(result);
            await _context.SaveChangesAsync();
            return Ok();

        }
    }
}

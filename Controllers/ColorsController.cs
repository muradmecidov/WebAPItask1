using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_API.DAL;
using WEB_API.Entities;
using WEB_API.Entities.Dtos.Cars;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ColorsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var color = _context.Colors.Find(id);
            if (color == null)
            {
                return NotFound();
            }

            return Ok(color);
        }
        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            var result = await _context.Colors.ToListAsync();
            if (result.Count == 0) { return NotFound(); }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddCars(CreateColorDto create)
        {
            Color color = new Color
            {
                Name = create.Name,
            };
            await _context.Colors.AddAsync(color);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut]
        public async Task<IActionResult> Update( UpdateColorDto update)
        {
            var result = await _context.Colors.FindAsync(update.Id);
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
            var result = await _context.Colors.FindAsync(id);
            if (result is null) {  return NotFound(); }
            _context.Colors.Remove(result);
            await _context.SaveChangesAsync();
            return Ok();

        }




    }
}

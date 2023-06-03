using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB_API.DAL;
using WEB_API.Entities.Dtos.Cars;
using WEB_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CarsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var color = _context.Cars.Find(id);
            if (color == null)
            {
                return NotFound();
            }

            return Ok(color);
        }
        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            var result = await _context.Cars.ToListAsync();
            if (result.Count == 0) { return NotFound(); }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddCars(CreateCarDto create)
        {
            Car car = new Car
            {
                BrandId = create.BrandId,
                ColorId = create.ColorId,
                ModelYear = DateTime.UtcNow,
                DailyPrice = create.DailyPrice,
                Description = create.Description,
            };
            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateCarDto update)
        {
            var result = await _context.Cars.FindAsync(update.Id);
            if (result is null)
            {
                return NotFound();
            }
            result.BrandId = update.BrandId;
            result.ColorId = update.ColorId;
            result.DailyPrice = update.DailyPrice;
            result.Description = update.Description;

            await _context.SaveChangesAsync();
            return NoContent();

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _context.Cars.FindAsync(id);
            if (result is null) { return NotFound(); }
            _context.Cars.Remove(result);
            await _context.SaveChangesAsync();
            return Ok();

        }
    }
}

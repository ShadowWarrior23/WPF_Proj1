using global::WPF_Proj1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Dtos;
using WPF_Proj1.Api.Dtos;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoodItemsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public FoodItemsController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<FoodItemDto>>> GetFoodItems([FromQuery] string? categ)
        {
            var query = _db.FoodItems.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(categ))
            {
                categ = categ.Trim().ToLower();
                query = query.Where(f => f.Categ.ToLower() == categ);
            }

            var foodItems = await query
                .OrderBy(f => f.Name)
                .Select(f => new FoodItemDto
                {
                    Name = f.Name,
                    Categ = f.Categ,
                    Ingreds = f.Ingreds,
                    Allergens = f.Allergens,
                    Tags = f.Tags
                })
                .ToListAsync();

            return Ok(foodItems);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<FoodItemDto>> GetFoodItem(string name)
        {
            var normalized = name.Trim().ToLower();

            var food = await _db.FoodItems
                .AsNoTracking()
                .Where(f => f.Name.ToLower() == normalized)
                .Select(f => new FoodItemDto
                {
                    Name = f.Name,
                    Categ = f.Categ,
                    Ingreds = f.Ingreds,
                    Allergens = f.Allergens,
                    Tags = f.Tags
                })
                .FirstOrDefaultAsync();

            if (food == null)
                return NotFound();

            return Ok(food);
        }
    }
}
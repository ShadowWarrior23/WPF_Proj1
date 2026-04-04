using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WPF_Proj1.Api.Dtos;

namespace WPF_Proj1.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly AppDbContext _db;

        public MenuController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<DailyMenuDto>>> GetMenus()
        {
            var menus = await _db.DailyMenus
                .OrderBy(m => m.Day)
                .Select(m => new DailyMenuDto
                {
                    Day = m.Day.ToString(),
                    Soup = m.Soup,
                    DishA = m.DishA,
                    DishB = m.DishB
                })
                .ToListAsync();

            return Ok(menus);
        }
    }
}
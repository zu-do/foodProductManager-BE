using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PVP_Projektas_API.Data;
using PVP_Projektas_API.Models;
namespace PVP_Projektas_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ProjectDbContext _dbContext;
        public CategoryController(ProjectDbContext db)
        {
            _dbContext = db;
        }
        [HttpGet("categories")]
        public async Task<List<Category>> Get()
        {

            return await _dbContext.DbCategories.ToListAsync();
        }
        [HttpPost("category")]
        public async Task<Category> Post([FromBody] string name)
        {
            var category = new Category()
            {
                CategoryName = name
            };
            await _dbContext.DbCategories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
            return category;
        }
    }
}
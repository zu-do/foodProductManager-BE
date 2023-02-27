using Microsoft.AspNetCore.Mvc;
using PVP_Projektas_API.Data;
using PVP_Projektas_API.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PVP_Projektas_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProjectDbContext _dbContext;

        public ProductController(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("products")]
        public async Task<ActionResult<Product>> Get()
        {
            return Ok(await _dbContext.DbProducts
                .Include(it => it.ProductCategory)
                .Select(it => new
                {
                    id = it.Id,
                    name = it.ProductName,
                    category = it.ProductCategory.CategoryName,
                    description = it.ProductDescription,
                    expiration = it.ExpirationTime,
                })
                .ToListAsync());
        }
        [HttpDelete("delete")]
        public async Task<ActionResult<Product>> Delete(int? id)
        {
            var product=_dbContext.DbProducts.FirstOrDefault(_dbContext => _dbContext.Id == id);
            _dbContext.DbProducts.Remove(product);
            _dbContext.SaveChangesAsync();
            return Ok();

        }
    }
    
}

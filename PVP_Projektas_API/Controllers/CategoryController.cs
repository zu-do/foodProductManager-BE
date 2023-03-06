using Microsoft.AspNetCore.Mvc;
using PVP_Projektas_API.Models;
using PVP_Projektas_API.Repository;

namespace PVP_Projektas_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet("getAll")]
        public async Task<List<Category>> GetAllCategoriesAsync() => await _categoryRepository.GetAllCategories();

        [HttpPost("add")]
        public async Task<Category> Post([FromBody] string name)
        {
            return await _categoryRepository.AddCategory(name);
        }

        [HttpDelete("delete")]
        public async Task<List<Category>?> DeleteCategory([FromBody] string name) => await _categoryRepository.DeleteCategory(name);
    }
}
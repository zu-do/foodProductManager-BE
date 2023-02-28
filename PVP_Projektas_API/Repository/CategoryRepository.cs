using Microsoft.EntityFrameworkCore;
using PVP_Projektas_API.Data;
using PVP_Projektas_API.Models;

namespace PVP_Projektas_API.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ProjectDbContext _dbContext;

        public CategoryRepository(ProjectDbContext db)
        {
            _dbContext = db;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _dbContext.DbCategories.ToListAsync();
        }

        public async Task<Category> AddCategory(string name)
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
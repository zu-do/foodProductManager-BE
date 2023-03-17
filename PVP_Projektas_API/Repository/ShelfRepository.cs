using PVP_Projektas_API.Data;
using PVP_Projektas_API.Interfaces;
using PVP_Projektas_API.Models;
using Microsoft.EntityFrameworkCore;

namespace PVP_Projektas_API.Repository
{
    public class ShelfRepository : IShelfRepository
    {
        private readonly ProjectDbContext _dbContext;

        public ShelfRepository(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

         public async Task<Shelf> CreateDefaultShelf()
        {
            var shelf = new Shelf()
            {
                Name = "Default"
            };
           await _dbContext.AddAsync(shelf);
           await _dbContext.SaveChangesAsync();

            return shelf;
        }

        public async Task<List<Shelf>> GetAllShelves()
        {
            return await _dbContext.DbShelves.ToListAsync();
        }
    }
}

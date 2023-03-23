using PVP_Projektas_API.Data;
using PVP_Projektas_API.Interfaces;
using PVP_Projektas_API.Models;
using Microsoft.EntityFrameworkCore;
using Azure.Core;

namespace PVP_Projektas_API.Repository
{
    public class ShelfRepository : IShelfRepository
    {
        private readonly ProjectDbContext _dbContext;

        public ShelfRepository(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ChangeProductShelf(Product product, int shelfId)
        {
            var shelfToAdd = await _dbContext.DbShelves.Where(sh => sh.Id == shelfId).FirstOrDefaultAsync();

            foreach (var shelf in _dbContext.DbShelves)
            {
                if (shelf.Products != null)
                {
                    foreach (var shelfProduct in shelf.Products)
                    {
                        if (shelfProduct.Id == product.Id)
                        {
                            shelf.Products.Remove(shelfProduct);
                        }
                    }
                }
            }

            if (shelfToAdd.Products == null)
            {
                shelfToAdd.Products = new List<Product>();
            }

            shelfToAdd.Products.Add(product);
            return true;
           
        }

        public async Task<Shelf> CreateDefaultShelf(User user)
        {
            var shelf = new Shelf()
            {
                Name = "Default",
                UserId = user.Id,
            };
           await _dbContext.AddAsync(shelf);

           await _dbContext.SaveChangesAsync();

           return shelf;
        }

        public async Task<Shelf> CreateShelf(User user, string name)
        {
            var shelf = new Shelf()
            {
                Name = name,
                UserId = user.Id,
            };
            await _dbContext.AddAsync(shelf);

            await _dbContext.SaveChangesAsync();

            return shelf;
        }

        public async Task<List<Shelf>> GetAllShelves()
        {
            return await _dbContext.DbShelves.Include(p => p.Products).ToListAsync();
        }
    }
}

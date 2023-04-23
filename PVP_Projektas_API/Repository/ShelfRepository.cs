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

        public async Task<List<Shelf>> GetAllShelves()
        {
            return await _dbContext.DbShelves.Include(p => p.Products).ToListAsync();
        }

        public async Task<List<Shelf>> GetUserShelves(User user)
        {
            return await _dbContext.DbShelves.Include(pr => pr.Products).Where(us => us.UserId == user.Id).ToListAsync();
        }
        public async Task<Shelf?> AddShelfAsync(string name, int userid)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            var shelf = new Shelf()
            {
                Name = name,
                UserId = userid,
            };

            await _dbContext.AddAsync(shelf);
            await _dbContext.SaveChangesAsync();

            return shelf;
        }

        public async Task<List<Shelf>> UpdateShelfAsync(ShelfDto updateshelf)
        {
            var dbShelf = await _dbContext.DbShelves.FindAsync(updateshelf.Id);
            if (dbShelf == null) return null;

            dbShelf.Name = updateshelf.Name;
            await _dbContext.SaveChangesAsync();

            return await _dbContext.DbShelves.ToListAsync();

        }

        public async Task<List<Shelf>?> DeleteShelf(int id, int userid)
        {

            var dbShelf = await _dbContext.DbShelves.FindAsync(id);
            if (dbShelf == null)
                return null;
            var shelfProducts = await _dbContext.DbProducts.Where(s => s.ShelfId == id).ToListAsync();
            var productsCount = shelfProducts.Count;
            if (productsCount > 0)
            {
                var DefaultShelfID = await _dbContext.DbShelves.Where(sh => sh.UserId == userid).OrderBy(sh => sh.Id).Select(sh => sh.Id).FirstOrDefaultAsync();

               // var shelfProducts = await _dbContext.DbProducts.Where(s => s.ShelfId == id).ToListAsync();
                foreach (var shelfProduct in shelfProducts)
                {
                    shelfProduct.ShelfId = DefaultShelfID;
                }
                await _dbContext.SaveChangesAsync();
                _dbContext.DbShelves.Remove(dbShelf);
            }
            else
                _dbContext.DbShelves.Remove(dbShelf);

            
            await _dbContext.SaveChangesAsync();

            return await _dbContext.DbShelves.ToListAsync();

           
        }
    }
}

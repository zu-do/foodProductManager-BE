using Microsoft.EntityFrameworkCore;
using PVP_Projektas_API.Data;
using PVP_Projektas_API.Interfaces;
using PVP_Projektas_API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web.Mvc;

namespace PVP_Projektas_API.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProjectDbContext _dbContext;

        public ProductRepository(ProjectDbContext db)
        {
            _dbContext = db;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            
            return await _dbContext.DbProducts.ToListAsync();
        }

        public async Task<List<Product>?> DeleteProduct(int id)
        {
            var dbProduct = await _dbContext.DbProducts.FindAsync(id);
            if (dbProduct == null)
                return null;

            _dbContext.DbProducts.Remove(dbProduct);
            await _dbContext.SaveChangesAsync();

            return await _dbContext.DbProducts.ToListAsync();
        }

        public async Task<List<Product>> UpdateProductAsync(UpdateProductDto request, int id)
        {
            var dbProduct = await _dbContext.DbProducts.FindAsync(id);
            if (dbProduct == null) return null;

            dbProduct.ProductName = request.ProductName;
            dbProduct.CategoryName = request.CategoryName;
            dbProduct.ProductDescription = request.ProductDescription;
            dbProduct.ExpirationTime = request.ExpirationTime;

            if(request.ShelfId != dbProduct.ShelfId)
            {
                var updatedShelf = await _dbContext.DbShelves.Where(sh => sh.Id == request.ShelfId).FirstOrDefaultAsync();
                var oldShelf = await _dbContext.DbShelves.Where(sh => sh.Id == dbProduct.ShelfId).FirstOrDefaultAsync();
                updatedShelf?.Products!.Add(dbProduct);
                oldShelf?.Products?.Remove(dbProduct);
            }

            await _dbContext.SaveChangesAsync();

            return await _dbContext.DbProducts.ToListAsync();
        }

        public async Task<Product> AddProductAsync(CreateProductDto request)
        {
            //var category = await _dbContext.DbCategories.FindAsync(request.CategoryName);

            var category = await _dbContext.DbCategories
                .Include(c => c.Products)
                .SingleOrDefaultAsync(c => c.CategoryName == request.CategoryName);

            if (category == null)
            {
                return null;
            }
            
            var newProduct = new Product
            {
                ProductName = request.ProductName,
                ProductDescription = request.ProductDescription,
                CategoryName = request.CategoryName,
                ExpirationTime = request.ExpirationTime,
                ShelfId = request.ShelfId,
                Givable = false,

            };
            
            _dbContext.DbProducts.Add(newProduct);

            var userShelf = await _dbContext.DbShelves.Where(sh => sh.Id == request.ShelfId).FirstOrDefaultAsync();

            if (userShelf.Products == null)
            {
                userShelf.Products = new List<Product>();
            }
            userShelf?.Products!.Add(newProduct);

            await _dbContext.SaveChangesAsync();

            return newProduct;
        }

        public Task<Product> GetProductById(int id)
        {
            var product = _dbContext.DbProducts.First(product => product.Id == id);

            return Task.FromResult(product);
        }

        public async Task<List<Product>> GetUserProducts(string email, int? shelf = null)
        {
            if (shelf is null)
            {
                var user = await _dbContext.DbUsers.Where(u => u.Email == email).FirstOrDefaultAsync();

                var defaultShelf = await _dbContext.DbShelves.Where(sh => sh.UserId == user.Id).OrderBy(sh => sh.Id).Include(sh => sh.Products).FirstAsync();

                var products = defaultShelf?.Products?.ToList();
                return products;
            }
            throw new NotImplementedException();
        }
    }
}
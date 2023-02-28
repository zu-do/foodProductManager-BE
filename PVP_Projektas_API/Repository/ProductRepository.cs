using Microsoft.EntityFrameworkCore;
using PVP_Projektas_API.Data;
using PVP_Projektas_API.Interfaces;
using PVP_Projektas_API.Models;

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

        public async Task<List<Product>> UpdateProductAsync(Product product)
        {
            var dbProduct = await _dbContext.DbProducts.FindAsync(product.Id);
            if (dbProduct == null) return null;

            dbProduct.ProductName = product.ProductName;
            dbProduct.ProductCategory.CategoryName = product.ProductCategory.CategoryName;
            dbProduct.ProductDescription = product.ProductDescription;
            dbProduct.ExpirationTime = product.ExpirationTime;

            await _dbContext.SaveChangesAsync();

            return await _dbContext.DbProducts.ToListAsync();
        }

        public async Task<List<Product>> AddProductAsync(Product product)
        {
            _dbContext.DbProducts.Add(product);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.DbProducts.ToListAsync();
        }

        public Task<Product> GetProductById(int id)
        {
            var product = _dbContext.DbProducts.First(product => product.Id == id);

            return Task.FromResult(product);
        }
    }
}
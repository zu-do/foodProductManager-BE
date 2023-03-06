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

        public async Task<List<Product>> UpdateProductAsync(CreateProductDto request, int id)
        {
            var dbProduct = await _dbContext.DbProducts.FindAsync(id);
            if (dbProduct == null) return null;

            dbProduct.ProductName = request.ProductName;
            dbProduct.CategoryName = request.CategoryName;
            dbProduct.ProductDescription = request.ProductDescription;
            dbProduct.ExpirationTime = request.ExpirationTime;

            await _dbContext.SaveChangesAsync();

            return await _dbContext.DbProducts.ToListAsync();
        }

        public async Task<List<Product>> AddProductAsync(CreateProductDto request)
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

            };
            
            _dbContext.DbProducts.Add(newProduct);

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
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
            var dbProduct = await _dbContext.DbProducts.FirstAsync(it => it.Id == id);
            if (dbProduct == null) return null;

            dbProduct.ProductName = request.ProductName;
            dbProduct.CategoryName = request.CategoryName;
            dbProduct.ProductDescription = request.ProductDescription;
            dbProduct.ExpirationTime = request.ExpirationTime;
            dbProduct.Quantity = request.Quantity;
            dbProduct.UnitTypeId = request.UnitTypeId;

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
                Quantity = request.Quantity,
                UnitTypeId = request.UnitTypeId,
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
        public async Task<List<Product>> GetGiveableProducts()
        {
            return await _dbContext.DbProducts.Where(pr => pr.Givable == true && pr.Reserved == false).ToListAsync();
        }
        
        public async Task<int> MarkReserved(int id, int takingUserID)
        {
            var dbProduct = await _dbContext.DbProducts.FirstAsync(it => it.Id == id);
            if (dbProduct == null) 
                return -1;
            dbProduct.Reserved= true;
            var productsUserID = await _dbContext.DbShelves
             .Where(s => s.Id == dbProduct.ShelfId)
             .Select(s => s.UserId)
             .FirstOrDefaultAsync();
            
            
            await _dbContext.SaveChangesAsync();
            return productsUserID;
        }

        public Task<DateTime?> SuggestDate(string product, string category)
        {
            if(ValidateProductName(product, category))
                return Task.FromResult<DateTime?>(null);
            string[] productwords = product.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            int productwordscount= CountProductWords(product);
            var categoryProducts = GetCategoryProducts(category);


            List<int> ExpirationTimes = new List<int>();
            foreach (var categoryProduct in categoryProducts)
            {
                int matchCount = 0;
                int i = 0;
                string[] Categoryproductwords = categoryProduct.ProductName.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int categoryPwords = CountCategoryProductsWords(categoryProduct);
                foreach (var categoryPword in Categoryproductwords)
                {
                    if (CheckWordsCount(categoryPword, productwords[i]))
                        matchCount++;
                    i++;
                }
                if( !CompareMatchNumber(productwordscount, categoryPwords))
                {
                    if (matchCount == 0 || matchCount < productwordscount)
                        continue;
                    if(matchCount == productwordscount)
                        ExpirationTimes = AddExpirationTime(ExpirationTimes, categoryProduct.DaysUntilExpiration);

                }
                if(CompareMatchNumber(productwordscount, categoryPwords))
                {
                    if (matchCount == 0 || matchCount < categoryPwords)
                        continue;
                    if (matchCount ==  categoryPwords)
                        ExpirationTimes = AddExpirationTime(ExpirationTimes, categoryProduct.DaysUntilExpiration);
                }
                
            }
            if (ExpirationTimes.Count > 0)
            {
                var averageTime = GetAverageOfExpirationTime(ExpirationTimes);
                DateTime suggestedDate = AddAverageToCurrentDate(averageTime);
                return Task.FromResult<DateTime?>(suggestedDate);
            }
            else
            {
                return Task.FromResult<DateTime?>(DateTime.Today.AddDays(5));
            }
        }
        private bool ValidateProductName(string product, string category)
        {
            return product == null && category == null;
        }
        private int CountProductWords(string product)
        {
            string[] productwords = product.Split(new string[] { "%20" }, StringSplitOptions.RemoveEmptyEntries);
            int productwordscount = productwords.Length;
            return productwordscount;
        }
        private List<Product> GetCategoryProducts(string category)
        {
            var categoryProducts = _dbContext.DbProducts.Where(u => u.CategoryName == category).ToList();
            return categoryProducts;
        }
        private int CountCategoryProductsWords(Product categoryProduct)
        {
            string[] Categoryproductwords = categoryProduct.ProductName.Split(new string[] { "%20" }, StringSplitOptions.RemoveEmptyEntries);
            int categoryProductwordCount = Categoryproductwords.Length;
            return categoryProductwordCount;
        }
        private bool CheckWordsCount(string categoryproduct, string product)
        {
            return string.Equals(categoryproduct, product, StringComparison.OrdinalIgnoreCase);
        }
        private bool CompareMatchNumber(int productwordsCount, int categoryproductwordscount)
        {
            if (productwordsCount >= categoryproductwordscount)
                return true;
            else
                return false;
        }
        private List<int> AddExpirationTime(List<int> list, int time)
        {
            list.Add(time);
            return list;
        }
        private double GetAverageOfExpirationTime(List<int> list)
        {
            return list.Average();
        }
        private DateTime AddAverageToCurrentDate(double average)
        {
            DateTime suggestedDate = DateTime.Today.AddDays(average);
            return suggestedDate;
        }
        public async Task<Product> ChangeState(int id, int addressId)
        {
            var product = await _dbContext.DbProducts.Where(p => p.Id == id).FirstOrDefaultAsync();
            var address = await _dbContext.DbAddresses.Include(c => c.Products).SingleOrDefaultAsync(c => c.Id == addressId);

            if (address == null) return null;

            product.AddressId = addressId;
            product.Givable = true;

            await _dbContext.SaveChangesAsync();
            return product;
        }
    }
}
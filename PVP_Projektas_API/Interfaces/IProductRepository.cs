using PVP_Projektas_API.Models;

namespace PVP_Projektas_API.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts();

        Task<List<Product>?> DeleteProduct(int id);

        Task<List<Product>> UpdateProductAsync(Product product);

        Task<List<Product>> AddProductAsync(Product product);

        Task<Product> GetProductById(int id);
    }
}
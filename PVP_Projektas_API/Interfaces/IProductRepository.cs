using PVP_Projektas_API.Models;

namespace PVP_Projektas_API.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts();

        Task<List<Product>?> DeleteProduct(int id);

        Task<List<Product>> UpdateProductAsync(UpdateProductDto request, int id);

        Task<Product> AddProductAsync(CreateProductDto request);

        Task<Product> GetProductById(int id);

        Task<List<Product>> GetUserProducts(string email, int? shelf = null);
    }
}
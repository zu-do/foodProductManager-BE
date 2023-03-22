using Microsoft.AspNetCore.Mvc;
using PVP_Projektas_API.Interfaces;
using PVP_Projektas_API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PVP_Projektas_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("getAll")]
        public async Task<List<Product>> GetAllProducts()
        {
            return await _productRepository.GetAllProducts();
        }

        [HttpPost("user-products")]
        public async Task<List<Product>> GetUserProducts([FromBody] UserProductsRequest request)
        {
            var email = request.Email;
            var shelf = request.Shelf;
            return await _productRepository.GetUserProducts(email, shelf);
        }

        [HttpDelete("delete")]
        public async Task<List<Product>?> DeleteProduct([FromBody] int id) => await _productRepository.DeleteProduct(id);

        [HttpPut("update")]
        public async Task<List<Product>?> UpdateProduct([FromBody] UpdateProductDto request, int id) => await _productRepository.UpdateProductAsync(request, id);

        [HttpPost("create")]
        public async Task<Product?> AddProductAsync([FromBody] CreateProductDto request) => await _productRepository.AddProductAsync(request);

    }
     public class UserProductsRequest
    {
        public string Email { get; set; }
        public int? Shelf { get; set; }
    }

}
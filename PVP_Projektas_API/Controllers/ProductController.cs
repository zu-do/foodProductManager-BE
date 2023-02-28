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

        [HttpDelete("delete")]
        public async Task<List<Product>?> DeleteProduct([FromBody] int id) => await _productRepository.DeleteProduct(id);

        [HttpPut("update")]
        public async Task<List<Product>?> UpdateProduct(Product product) => await _productRepository.UpdateProductAsync(product);

        [HttpPost("create")]
        public async Task<List<Product>?> AddProductAsync(Product product) => await _productRepository.AddProductAsync(product);

        [HttpPost("GetProductById")]
        public async Task<Product> AddProductAsync([FromBody] int id) => await _productRepository.GetProductById(id);
    }
}
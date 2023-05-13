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
        private readonly IReservationRepository _reservationRepository;
        private readonly IMailService _mailService;
        public ProductController(IProductRepository productRepository, IReservationRepository reservationRepository, IMailService mailService)
        {
            _productRepository = productRepository;
            _reservationRepository = reservationRepository;
            _mailService = mailService;
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

        [HttpGet("getGiveable")]
        public async Task<List<Product>?> GetGiveableProducts() => await _productRepository.GetGiveableProducts();

        [HttpPut("reserve/{id}/{userid}")]
        public async Task<Reservation> ReserveProduct([FromRoute] int id, [FromRoute] int userid)
        {
            if (id < 0)
                return null;

            var givinguserID = await _productRepository.MarkReserved(id, userid);
            var product = await _productRepository.GetProductById(id);
            if (givinguserID >= 0)
            {
                await _mailService.SendMail("brook37@ethereal.email", "Rezervuotas produktas", $"Jūsų produktas, pavadinimu \"{product.ProductName}\", buvo rezervuotas.");
                return await _reservationRepository.Create(id, givinguserID, userid);
            }
            return null;
        }
        [HttpGet("{product}/{category}")]
        public async Task<DateTime?> SuggestDate([FromRoute] string product, [FromRoute] string category)
        {
            var suggestedDate = await _productRepository.SuggestDate(product, category);
            return suggestedDate;
        }
    }
     public class UserProductsRequest
    {
        public string Email { get; set; }
        public int? Shelf { get; set; }
    }

}
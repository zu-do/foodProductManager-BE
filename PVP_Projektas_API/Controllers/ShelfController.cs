using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PVP_Projektas_API.Interfaces;
using PVP_Projektas_API.Models;

namespace PVP_Projektas_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShelfController : ControllerBase
    {
        private readonly IShelfRepository _shelfRepository;

        public ShelfController(IShelfRepository shelfRepository)
        {
            _shelfRepository = shelfRepository;
        }

        [HttpGet("getAll")]
        public async Task<List<Shelf>> GetAllShelves()
        {
            return await _shelfRepository.GetAllShelves();
        }
    }
}

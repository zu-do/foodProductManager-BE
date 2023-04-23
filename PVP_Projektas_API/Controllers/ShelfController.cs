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
        private readonly IUserRepository _userRepository;

        public ShelfController(IShelfRepository shelfRepository, IUserRepository userRepository)
        {
            _shelfRepository = shelfRepository;
            _userRepository = userRepository;
        }

        [HttpGet("getAll")]
        public async Task<List<Shelf>> GetAllShelves()
        {
            return await _shelfRepository.GetAllShelves();
        }

        [HttpGet("getAll/{email}")]
        public async Task<ActionResult> GetAllShelvesByEmail([FromRoute] string email)
        {
            var user = await _userRepository.GetUser(email);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(await _shelfRepository.GetUserShelves(user));
        }
        [HttpPost("create/{shelfName}/{UserID}")]

        public async Task<Shelf?> AddShelfAsync([FromRoute] string shelfName, [FromRoute] int UserID)
        {
            return await _shelfRepository.AddShelfAsync(shelfName, UserID);
        }
        [HttpPut("update")]
        public async Task<List<Shelf>?> UpdateShelf([FromBody] ShelfDto update)
        {
            return await _shelfRepository.UpdateShelfAsync(update);
        }
        [HttpDelete("delete/{id}/{userid}")]
        public async Task<List<Shelf>?> DeleteShelf([FromRoute] int id, [FromRoute] int userid)
        {
            return await _shelfRepository.DeleteShelf(id, userid);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using PVP_Projektas_API.Interfaces;
using PVP_Projektas_API.Models;

namespace PVP_Projektas_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IShelfRepository _shelfRepository;
        private readonly IAdminRepository _adminRepository;
        public UserController(IUserRepository userRepository, IShelfRepository shelfRepository, IAdminRepository adminRepository)
        {
            _userRepository = userRepository;
            _shelfRepository = shelfRepository;
            _adminRepository = adminRepository;
        }

        [HttpGet("{email}")]
        public async Task<ActionResult> GetUser([FromRoute] string email)
        {
           var user = await _userRepository.GetUser(email);
            
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("login/{email}/{password}")]
        public async Task<ActionResult> LoginUser([FromRoute] string email, [FromRoute] string password)
        {
            var user = await _userRepository.GetUser(email);

            if (user == null)
            {
                return NotFound();
            }

            if (password is not null && password == user.Password)
            {

                return Ok(user);
            }
            return Unauthorized();
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] UserDto userDto)
        {
            if (await _userRepository.GetUser(userDto.Email) is null && await _adminRepository.GetAdmin(userDto.Email) is null)
            {
                var user = await _userRepository.CreateUser(userDto);

                await _shelfRepository.CreateDefaultShelf(user);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            else return BadRequest();
        }

        [HttpGet("users")]
        public async Task<List<User>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }

        [HttpGet("products/{email}")]
        public async Task<List<User>> GetUserProducts([FromRoute] string email)
        {
            throw new NotImplementedException();
        }


    }
}

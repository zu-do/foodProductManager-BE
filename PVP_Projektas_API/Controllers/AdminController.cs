using Microsoft.AspNetCore.Mvc;
using PVP_Projektas_API.Interfaces;
using PVP_Projektas_API.Models;

namespace PVP_Projektas_API.Controllers;

[Route("[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly IAdminRepository _adminRepository;
  
    public AdminController(IAdminRepository adminRepository)
    {
        _adminRepository = adminRepository;
    }
    [HttpPost("create")]
    public async Task CreateAdmin([FromBody] UserDto data)
    {
        await _adminRepository.CreateAdmin(data);
    }

    [HttpGet("login/{email}/{password}")]
    public async Task<ActionResult> LoginUser([FromRoute] string email, [FromRoute] string password)
    {
        var admin = await _adminRepository.GetAdmin(email);

        if (admin == null)
        {
            return NotFound();
        }

        if (password is not null && password == admin.Password)
        {

            return Ok(admin);
        }
        return BadRequest();
    }
}

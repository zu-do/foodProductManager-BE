using Microsoft.AspNetCore.Mvc;
using PVP_Projektas_API.Interfaces;
using PVP_Projektas_API.Models;

namespace PVP_Projektas_API.Controllers;

[ApiController]
[Route("[controller]")]
public class RecipesController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IRecipesClient _recipesClient;

    public RecipesController(IUserRepository userRepository,IRecipesClient recipesClient)
    {
        _userRepository = userRepository;
        _recipesClient = recipesClient;
    }

    [HttpGet("email")]
    public async Task<ActionResult<List<Recipe>>> Get(string email)
    {
        var recipes = await _recipesClient.GetRecipes();
        var userProducts = _userRepository.GetUserProducts(email);
        return Ok(recipes);
    }
}

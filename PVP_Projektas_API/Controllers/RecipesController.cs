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
    private readonly IRecipesRepository _recipesRepository;

    public RecipesController(IUserRepository userRepository,IRecipesClient recipesClient, IRecipesRepository recipesRepository)
    {
        _userRepository = userRepository;
        _recipesClient = recipesClient;
        _recipesRepository = recipesRepository;
    }

    [HttpGet("{email}")]
    public async Task<ActionResult<List<RecipeDto>>> Get([FromRoute] string email)
    {
        var recipes = await _recipesClient.GetRecipes();
        var userProducts = await _userRepository.GetUserProducts(email);
        var fitRecipes = await _recipesRepository.RecommendRecipes(recipes, userProducts, email);

        if (fitRecipes is not null)
        {
            return Ok(fitRecipes);
        }
        return NotFound();
    }

    [HttpGet("v2/{email}")]
    public async Task<ActionResult<List<Recipe>>> GetV2([FromRoute] string email)
    {
        var fitRecipes = await _recipesRepository.RecommendRecipesV2(email);

        if (fitRecipes is not null)
        {
            return Ok(fitRecipes);
        }
        return NotFound();
    }
}

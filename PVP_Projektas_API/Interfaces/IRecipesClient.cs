using PVP_Projektas_API.Models;

namespace PVP_Projektas_API.Interfaces;

public interface IRecipesClient
{
    Task<List<Recipe>> GetRecipes();
}

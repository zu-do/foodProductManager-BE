using PVP_Projektas_API.Models;

namespace PVP_Projektas_API.Interfaces;

public interface IRecipesClient
{
    Task<List<Recipe>> GetRecipes();
    Task<List<Recipe>> GetRecipesV2(string product);
}

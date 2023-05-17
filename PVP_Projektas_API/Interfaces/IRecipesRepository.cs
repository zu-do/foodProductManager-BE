using PVP_Projektas_API.Models;

namespace PVP_Projektas_API.Interfaces;

public interface IRecipesRepository
{
    public Task<List<RecipeDto>> RecommendRecipes(List<Recipe> recipes, List<Product> products, string email);
}

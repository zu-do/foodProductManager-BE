using PVP_Projektas_API.Models;

namespace PVP_Projektas_API.Interfaces;

public interface ITranslationService
{
    public Task<List<Recipe>> TranslateRecipes(List<Recipe> recipe);
}

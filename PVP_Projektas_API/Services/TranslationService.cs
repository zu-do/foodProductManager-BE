using PVP_Projektas_API.Interfaces;
using PVP_Projektas_API.Models;

namespace PVP_Projektas_API.Services;

public class TranslationService : ITranslationService
{
    private readonly ITranslationClient _client;
    public TranslationService(ITranslationClient client)
    {
        _client = client;
    }
    public async Task<List<Recipe>> TranslateRecipes(List<Recipe> recipes)
    {
        var recipesCsv = string.Empty;

        for (int i = 0; i < recipes.Count; i++)
        {
            if (i < recipes.Count - 1)
            {
                recipesCsv = recipesCsv + recipes[i].Name + ";";
            }
            else
            {
                recipesCsv = recipesCsv + recipes[i].Name;
            }
        }

        var recipesNameCsv = await _client.TranslateFromEnToLt(recipesCsv);

        var names = recipesNameCsv.Split(";").ToList();

        for (int i = 0; i < recipes.Count; i++)
        {
            recipes[i].Name = names[i];
        }

        return recipes;

        //recipe.Name = await _client.TranslateFromEnToLt(recipe.Name);

        //var ingredientsCsv = string.Empty;

        //for (int i = 0; i < recipe.Ingredients.Count; i++)
        //{
        //    if (i < recipe.Ingredients.Count - 1)
        //    {
        //        ingredientsCsv = ingredientsCsv + recipe.Ingredients[i] + ";";
        //    }
        //    else
        //    {
        //        ingredientsCsv = ingredientsCsv + recipe.Ingredients[i];
        //    }
        //}

        //var enIngredientsCsv = await _client.TranslateFromEnToLt(ingredientsCsv);

        //var ingredients = enIngredientsCsv.Split(";").ToList();

        //for (int i = 0; i < recipe.Ingredients.Count; i++)
        //{
        //    recipe.Ingredients[i] = ingredients[i];
        //}

        //return recipe;
    }
}

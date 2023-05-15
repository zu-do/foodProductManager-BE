using PVP_Projektas_API.Interfaces;
using PVP_Projektas_API.Models;

namespace PVP_Projektas_API.Repository;

public class RecipesRepository : IRecipesRepository
{
    public List<Recipe> RecommendRecipes(List<Recipe> recipes, List<Product> products)
    {
        List<Recipe> fitRecipes = new List<Recipe>();
        var ingredients = products.Select(p => p.ProductName).ToList();

        foreach (var recipe in recipes)
        {
            foreach (var product in products)
            {
                product.ExistsInRecipe = false;
                if (CheckIfProductContainedInRecipe(recipe.Ingredients, product.ProductName))
                {
                    product.ExistsInRecipe = true;
                }
            }
            

            if (CountExisting(recipe, products) == recipe.Ingredients.Count)
            {
                fitRecipes.Add(recipe);
            }
            else if (CountExisting(recipe, products) + 1 == recipe.Ingredients.Count)
            {
                var missingProduct = products.Find(p => p.ExistsInRecipe == false);
                throw new NotImplementedException("1 is missing");
            }
        }

        return fitRecipes;
    }
    private bool CheckIfProductContainedInRecipe(List<string> ingredients, string product)
    {
        if (ingredients.Contains(product))
        {
            return true;
        }

        return false;
    }

    private int CountExisting(Recipe recipe, List<Product> products)
    {
        int fiting = 0;

        foreach(var product in products)
        {
            if (product.ExistsInRecipe)
            {
                fiting++;
            }
        }

        return fiting;
    }
}

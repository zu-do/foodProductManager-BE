using PVP_Projektas_API.Interfaces;
using PVP_Projektas_API.Models;
using Newtonsoft.Json.Linq;

namespace PVP_Projektas_API.Clients;
public class RecipesClient : IRecipesClient
{
    //"https://api.edamam.com/api/recipes/v2?type=public&q=chicken&app_id=a5bbca95&app_key=43a3fd72548a08fa19c658f36265bfcf
    private readonly HttpClient _httpClient;
    private readonly string _url = "https://api.edamam.com/api/recipes/v2?type=public";
    private readonly string _appIdKey = "&app_id=a5bbca95&app_key=43a3fd72548a08fa19c658f36265bfcf";

    public RecipesClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<List<Recipe>> GetRecipes()
    {
        var response = await _httpClient.GetAsync($"{_url}{_appIdKey}&q=food&field=label&field=ingredientLines&field=image&field=url&random=true");

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var json = await response.Content.ReadAsStringAsync();

        var recipes = JObject.Parse(json)["hits"].Select(hit => hit["recipe"]).ToList();

        var result = new List<Recipe>();
        AddRecipe(result);
        foreach (var r in recipes)
        {
            var recipe = new Recipe
            {
                Name = (string)r["label"],
                Ingredients = r["ingredientLines"].Select(i => (string)i).ToList(),
                ImageUrl = (string)r["image"],
                RecipeUrl = (string)r["url"],
            };

            result.Add(recipe);
        }

        return result;

    }









































    private List<Recipe> AddRecipe(List<Recipe> recipes)
    {
        var ing = new List<string> { "¾ cup oatmeal (not instant)", "flour", "salt", "warm water"};
        var recipe = new Recipe
        {
            ImageUrl = "https://media.istockphoto.com/id/537316530/photo/healthy-breakfast-organic-oat-flakes-in-a-wooden-bowl.jpg?s=612x612&w=0&k=20&c=Q2yB-W69cJk_W3yJHeM2OPvIQ7T3F3VAC2UlNhgRRug=",
            Ingredients = ing,
            Name = "Foods For St Patrick's Day - Oatcakes Recipe",
            RecipeUrl = "http://confessionsofanover-workedmom.com/foods-for-st-patricks-day-oatcakes-recipe/"

        };

        recipes.Add(recipe);

        return recipes;
    }
}

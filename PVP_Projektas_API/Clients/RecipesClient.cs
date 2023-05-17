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
            ImageUrl = "https://edamam-product-images.s3.amazonaws.com/web-img/a75/a7596766426b0d75c6464a74c6c045f4.jpg?X-Amz-Security-Token=IQoJb3JpZ2luX2VjEA0aCXVzLWVhc3QtMSJIMEYCIQC5%2FPj8zFgPxM3rms501CJOH13G0yXfyQiH8qJHPiiwdgIhAMuwjkR6KgAW30uzlk2GDLzLcyPGKw6p%2F0J3zrNarCImKrgFCDUQABoMMTg3MDE3MTUwOTg2IgyZk9UBqQSQBT9ciZIqlQW5v5FS5zrUnCUQW7wlpgFqrq30KdMB5viDliieOIylzT5DZbHp0dzu3fWFBxDaP%2FUkq9KkWLaD%2BjJFp13g5oiJreOpoJeCVDrzJ%2BZnncYr%2F598%2FZb%2FBnRoB6utloVz87N9oFp%2BOSAmfGoWcc0fDC8%2BqoQDDAK5%2F9OfvQJGuhZ3%2BPn4nBMAg1Et%2BMW8yayU4snRCdEWoQAgmayy8gYF2NqQFaaeHrurrsmJAbIrwpUOqwl%2B8PHokFkGLkUHO7q7Wy7c%2FEXvXJkmiwmoXv7vYSlkRG13JjOT3Xb7dZpTDcGHnfxKa96tQG284GeAX85ZPgef7KaPbjALVCtDNtQ07RrSigjtxnx96G6X%2B1pt%2FbUqThPk0AGtxW3RjF1nNm0Hxd0uFGqUZo1p8dO1ZUvMCBOJUviITFIHKaWMoX1iL9zd9BkUlNbr5OPQEnsQcxwRl4n1c82KvUzVxZoQTqsCBdtVAmqa%2FYmDZakvpbtpUigZfUMjbQBb%2BJRjP1r%2FF4aliyFxWRwfBeg%2FDZr%2F0WIqwtW4nzbs3MfCdEe2BuVvFZMniXk8SRHTyD4FZJWV3wokIbBk7PVVHd1q0zW1gRe0uTmFmPptFpOw7nYQnZp7YsMI0y8hRx1VF5o%2Fjr0re%2FNyhNvkUElcCgvt2GgsT0gytc9LhWHMevGDWLL3VxQcuCW4zgi0qoUnW4OzFudFlKY2HbFKSwvRArpJo%2BFDrmuR%2BEsyn0j6jnKKbU2skm%2FRMDsgW8qk3KbL1LPeCIYnA4ZIj%2Bc%2BVnlzFahO85bLOTOJmkbUJ7DhKjtrP40YEtU7KsCcUc2B4kEWQmLKcjR6tTdq5wztEuTMU4GTNbVNmrJlCy0J%2FM4oaFIrTxWMUyIeKKIv02Cg0ZRXMKXEj6MGOrABliSU%2FlihUnku%2BpqlRqDgkBE%2BlvJaswzvIxjLFTO5TOWHh0uSFfAB0EC1LmPMQ%2BTLWmFbNlYxAXolrw85Uk%2FgCwqUAA8OJkV%2F%2BhA7%2BGzUlV6MfhWpKsrZgNnd5mOYVbxdL%2Bl%2Br5TMKEGI%2BDcFg4OtyL7Fa3bYtIbs%2BwTO6QPwTY%2BMmJaKIXq5VBuHyT58XcewRd%2FTaX5%2FQOVbCGnzumnmd4znzZmd2gx7v0M2ocwTuGU%3D&X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Date=20230516T203929Z&X-Amz-SignedHeaders=host&X-Amz-Expires=3600&X-Amz-Credential=ASIASXCYXIIFALRJ6ZNT%2F20230516%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Signature=cde4dca60354b33fb88bf2728dc2b98302203e39f6b3c72bfac4faf52f1435d5",
            Ingredients = ing,
            Name = "Foods For St Patrick's Day - Oatcakes Recipe",
            RecipeUrl = "http://confessionsofanover-workedmom.com/foods-for-st-patricks-day-oatcakes-recipe/"

        };

        recipes.Add(recipe);

        return recipes;
    }
}

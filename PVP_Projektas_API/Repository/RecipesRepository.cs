using PVP_Projektas_API.Interfaces;
using PVP_Projektas_API.Models;

namespace PVP_Projektas_API.Repository;

public class RecipesRepository : IRecipesRepository
{
    private readonly IProductRepository _productsRepository;
    private readonly IDistanceClient _distanceClient;
    private readonly IUserRepository _userRepository;

    public RecipesRepository(IProductRepository productsRepository, IDistanceClient distanceClient, IUserRepository userRepository)
    {
        _productsRepository = productsRepository;
        _distanceClient = distanceClient;
        _userRepository = userRepository;
    }
    public async Task<List<RecipeDto>> RecommendRecipes(List<Recipe> recipes, List<Product> products, string email)
    {
        List<RecipeDto> fitRecipes = new List<RecipeDto>();
        var user = await _userRepository.GetUser(email);

        foreach (var recipe in recipes)
        {
            foreach (var product in products)
            {
                product.ExistsInRecipe = false;
                if (product.Reserved is false && product.Givable is false && CheckIfProductContainedInRecipe(recipe.Ingredients, product.ProductName))
                {
                    product.ExistsInRecipe = true;
                }
            }

            if (CountExisting(products, recipe) >= recipe.Ingredients.Count)
            {
                RecipeDto dto = new RecipeDto { Recipe = recipe };
                fitRecipes.Add(dto);
            }
            else if (CountExisting(products, recipe) + 1 == recipe.Ingredients.Count) // 1 products is missing to a recipe
            {
                double distance = 5;
                Product fititngProductInGiveAway = new Product();
                var giveawayProducts = await GetProductsForGiveAway();

                var missingProduct = CheckMissingProductInProducts(products, recipe);

                if (missingProduct is null)
                {
                    throw new Exception("Shit happens");
                }


                if (giveawayProducts.Count != 0)
                {
                    var giveawayProductsNames = giveawayProducts.Select(p => p.ProductName.ToLower()).ToList();
                    if (giveawayProductsNames.Contains(missingProduct.ToLower()))
                    {
                        var misisngProductsInGiveAway = giveawayProducts.Where(p => p.ProductName.ToLower() == missingProduct.ToLower()).ToList();

                        if (user.Addresses is not null)
                        {
                            foreach (var productInGiveAway in misisngProductsInGiveAway)
                            {
                                foreach (var address in user.Addresses)
                                {
                                    var calculatedDistance = _distanceClient.GetDistance((double)address.Latitude, (double)address.Longitude, (double)productInGiveAway.ProductAddress.Latitude, (double)productInGiveAway.ProductAddress.Longitude);
                                    if (calculatedDistance < distance)
                                    {
                                        distance = calculatedDistance;
                                        fititngProductInGiveAway = productInGiveAway;
                                    }
                                }
                            }
                        }
                    }
                }
                if (distance < 2 && fititngProductInGiveAway is not null)
                {
                    RecipeDto dto = new RecipeDto { Recipe = recipe, Product = fititngProductInGiveAway };
                    fitRecipes.Add(dto);
                }
            }
        }

        return fitRecipes;
    }
    private bool CheckIfProductContainedInRecipe(List<string> ingredients, string product)
    {
        foreach(var ingredient in ingredients)
        {
            if (ingredient.ToLower().Contains(product.ToLower()))
            {
                return true;
            }

        }
        return false;
    }

    //private int CountExisting(List<Product> products)
    //{
    //    int fiting = 0;

    //    foreach (var product in products)
    //    {
    //        if (product.ExistsInRecipe)
    //        {
    //            fiting++;
    //        }
    //    }

    //    return fiting;
    //}

    private int CountExisting(List<Product> products, Recipe recipe)
    {
        var count = 0;

        foreach(var ingredient in recipe.Ingredients)
        {
            foreach(var product in products)
            {
                if (product.ExistsInRecipe && ingredient.ToLower().Contains(product.ProductName.ToLower()))
                { 
                    count++;
                    break;
                }
            }
        }

        return count;
    }

    private async Task<List<Product>> GetProductsForGiveAway()
    {
        return await _productsRepository.GetGiveableProducts();
    }

    private string CheckMissingProductInProducts(List<Product> products, Recipe recipe)
    {
        return recipe.Ingredients.Where(i => !products.Any(p => p.ExistsInRecipe && i.ToLower().Contains(p.ProductName.ToLower()))).Single();
    }
}

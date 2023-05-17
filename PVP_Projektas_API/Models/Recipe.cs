namespace PVP_Projektas_API.Models;

public class Recipe
{
    public string Name {get; set;}
    public List<string> Ingredients { get; set; } = new List<string>();
    public string ImageUrl { get; set;} = string.Empty;
    public string RecipeUrl { get; set; } = string.Empty;

}

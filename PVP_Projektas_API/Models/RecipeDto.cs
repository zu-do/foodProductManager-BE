namespace PVP_Projektas_API.Models;

public class RecipeDto
{
    public Recipe Recipe { get; set; } = null!;
    public Product? Product { get; set; }
}

#nullable enable
namespace PVP_Projektas_API.Models;

public class Product
{
    public int Id { get; set; }
    public string ProductName { get; set; } = null!;
    public Category ProductCategory { get; set; } = null!;  
    public string? ProductDescription { get; set; }
    public int ExpirationTime { get; set; } = 0; // Days left before expiration
}

#nullable enable

namespace PVP_Projektas_API.Models;

/// <summary>
/// Contains all user products
/// </summary>
public class Shelf
{
    public int Id { get; set; }
    public List<Product>? Products { get; set; }
}
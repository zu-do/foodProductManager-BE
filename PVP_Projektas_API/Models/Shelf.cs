#nullable enable

using System.Text.Json.Serialization;

namespace PVP_Projektas_API.Models;

/// <summary>
/// Contains all user products
/// </summary>
public class Shelf
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int UserId { get; set; }

    [JsonIgnore]
    public virtual User User { get; set; } = null!;
    public ICollection<Product>? Products { get; set; }
}
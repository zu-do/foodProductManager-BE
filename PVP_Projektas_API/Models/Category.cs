#nullable enable

using Microsoft.EntityFrameworkCore;

namespace PVP_Projektas_API.Models;

/// <summary>
/// Food category (type f.e. diary, meat and so on)
/// </summary>
[PrimaryKey(nameof(CategoryName))]
public class Category
{
    public string CategoryName { get; set; } = null!;

    //Navigation properties
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
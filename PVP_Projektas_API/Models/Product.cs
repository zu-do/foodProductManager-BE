#nullable enable
using System.Text.Json.Serialization;

namespace PVP_Projektas_API.Models;

public class Product
{
    public int Id { get; set; }
    public string ProductName { get; set; } = null!;
    public string? ProductDescription { get; set; }
    public DateTime ExpirationTime { get; set; }
    public bool Givable { get; set; }
    public bool Reserved { get; set; }
    public decimal Quantity { get; set; }

    //Navigation properties

    public int ShelfId { get; set; }
    [JsonIgnore]
    public Shelf ProductShelf { get; set; } = null;

    public int? AddressId { get; set; }
    [JsonIgnore]
    public Address? ProductAddress { get; set; } = null;

    public string CategoryName { get; set; } = null!;
    [JsonIgnore]
    public Category ProductCategory { get; set; } = null;
    public int? UnitTypeId { get; set; }
    public UnitType? UnitType { get; set; }
    public int DaysUntilExpiration
    {
        get
        {
            TimeSpan timeRemaining = ExpirationTime.Date - DateTime.Today;
            return (int)timeRemaining.TotalDays;
        }
    }
}
public class UnitType
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Product>? Products { get; set; }
}
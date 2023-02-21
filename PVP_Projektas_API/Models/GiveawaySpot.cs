#nullable enable
namespace PVP_Projektas_API.Models;
/// <summary>
/// Place where you give away products
/// </summary>
public class GiveawaySpot
{
    public int Id { get; set; }
    public string Address { get; set; } = null!;
    public DateTime OpeningHours { get; set; }
    public DateTime ClosingHours { get; set; }

}

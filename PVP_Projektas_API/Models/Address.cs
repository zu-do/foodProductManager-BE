using System.Text.Json.Serialization;

namespace PVP_Projektas_API.Models
{
    public class Address
    {
        public int Id { get; set; } 
        public string Name { get; set; } = null!;
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public string? Comment { get; set; }
        public int UserId { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; } = null!;

    }
}

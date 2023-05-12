using System.Text.Json.Serialization;
namespace PVP_Projektas_API.Models

{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool Done { get; set; }

        public int GivesUserId { get; set; }
        public int TakesUserId { get; set; }
        public int ProductId { get; set; }

        [JsonIgnore]
        public virtual User GivesUser { get; set; } = null!;
        public virtual User TakesUser { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}

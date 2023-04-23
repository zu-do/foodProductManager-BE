namespace PVP_Projektas_API.Models
{
    public class CreateProductDto
    {
        public string ProductName { get; set; } = null!;
        public string? ProductDescription { get; set; }
        public DateTime ExpirationTime { get; set; }
        public string CategoryName { get; set; } = null!;
        public decimal Quantity { get; set; }
        public int ShelfId { get; set; }
        public int UnitTypeId { get; set; }

    }
    public class UpdateProductDto
    {
        public string ProductName { get; set; } = null!;
        public string? ProductDescription { get; set; }
        public DateTime ExpirationTime { get; set; }
        public string CategoryName { get; set; } = null!;
        public decimal Quantity { get; set; }

        public int ShelfId { get; set; }
        public int UnitTypeId { get; set; }

    }
}
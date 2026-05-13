using System.ComponentModel.DataAnnotations;

namespace InventoryApp.DTO
{
    public class UpdateProductDto
    {
        public required string Name { get; set; }

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public required string Category { get; set; }

        public int StockCount { get; set; }

        public int SupplierId { get; set; }
    }
}

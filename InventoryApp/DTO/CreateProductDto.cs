using System.ComponentModel.DataAnnotations;

namespace InventoryApp.DTO
{
    public class CreateProductDto
    {
        [Required]
        public required string Name { get; set; }

        public string Description { get; set; } = string.Empty;

        [Required]
        public required decimal Price { get; set; }

        [Required]
        public required string Category { get; set; }

        [Required]
        public int StockCount { get; set; }

        // Foreign key to Supplier
        [Required]
        public required int SupplierId { get; set; }
    }
}

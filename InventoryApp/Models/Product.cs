using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InventoryApp.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public required string Name { get; set; }

        public string Description { get; set; } = string.Empty;

        public required decimal Price { get; set; }

        public required string Category { get; set; }

        public int StockCount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Foreign key to Supplier
        public int SupplierId { get; set; }

        // Navigation property for related supplier (object reference used by EF)
        [JsonIgnore]
        [ForeignKey("SupplierId")]
        public Supplier? Supplier { get; set; }
    }
}

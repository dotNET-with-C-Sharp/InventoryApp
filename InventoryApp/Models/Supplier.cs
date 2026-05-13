using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace InventoryApp.Models
{
    public class Supplier
    {
        [Key]
        public int Id { get; set; }

        public required string Firstname { get; set; }

        public string Lastname { get; set; } = string.Empty;

        public required string CompanyName { get; set; }

        public int? Founded { get; set; }

        public string Description { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Navigation collection property for related products with initialization
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}

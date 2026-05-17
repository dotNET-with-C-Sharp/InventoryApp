using System.ComponentModel.DataAnnotations;

namespace InventoryApp.DTO
{
    public class CreateSupplierDto
    {
        [Required, StringLength(50)]
        public required string Firstname { get; set; } = string.Empty;

        [StringLength(50)]
        public string Lastname { get; set; } = string.Empty;

        public IFormFile? Image { get; set; }

        [Required, StringLength(50)]
        public required string CompanyName { get; set; }

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [Range(1700,2027)]
        public int? Founded { get; set; }
    }
}

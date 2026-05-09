using Microsoft.EntityFrameworkCore;
using InventoryApp.Models;

namespace InventoryApp.Data
{
    public class SupplierContext : DbContext
    {
        // Create a constructor that accepts DbContextOptions and passes it to the base class
        public SupplierContext(DbContextOptions<SupplierContext> options) : base(options) { }

        // Define a DbSet for the Supplier entity
        public DbSet<Supplier> Suppliers { get; set; }
    }
}

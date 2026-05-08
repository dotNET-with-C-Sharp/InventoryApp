using InventoryApp.Models;

namespace InventoryApp.Data
{
    public static class SupplierStore
    {
        private static readonly List<Supplier> _suppliers = new()
        {
            new Supplier { Id = 1, Firstname = "John", Lastname = "", CompanyName = "Bridgestone", Description = "" },
            new Supplier { Id = 2, Firstname = "Adam", Lastname = "", CompanyName = "MRF", Description = "" }
        };

        public static List<Supplier> GetAllSuppliers() => _suppliers;
    }
}

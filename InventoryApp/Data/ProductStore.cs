using InventoryApp.Models;

namespace InventoryApp.Data
{
    public class ProductStore
    {
        private static readonly List<Product> _products = new()
        {
            new Product { Id = 1, Name = "Ambrane", Description = "10000mAh powerbank", Price = 14.50M, Category = "Electronics", StockCount = 24 }
        };

        public static List<Product> GetAllProducts() => _products;
    }
}

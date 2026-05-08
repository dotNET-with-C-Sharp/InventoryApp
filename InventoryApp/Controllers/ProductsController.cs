using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InventoryApp.Models;
using InventoryApp.Data;

namespace InventoryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private List<Product> products = ProductStore.GetAllProducts();

        [HttpGet]
        public ActionResult <List<Product>> GetProducts()
        {
            return Ok(products);
        }
    }
}

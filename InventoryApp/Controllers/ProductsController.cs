using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InventoryApp.Models;
using InventoryApp.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }


        /* ---------------------------- 
         * GET /api/Products
         * ---------------------------- */
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();

            if (products == null || products.Count == 0)
                return BadRequest("No products found.");

            return Ok(new
            {
                message = "Products retrieved successfully.",
                products
            });

        }
    }
}

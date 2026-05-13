using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InventoryApp.Models;
using InventoryApp.Data;
using Microsoft.EntityFrameworkCore;
using InventoryApp.DTO;

namespace InventoryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context) => _context = context;


        /* ---------------------------- 
         * GET /api/Products
         * ---------------------------- */
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            //var products = await _context.Products.ToListAsync();

            var products = await _context.Products.Include(p => p.Supplier).ToListAsync();

            if (products == null || products.Count == 0)
                return NotFound("No products found.");

            return Ok(new
            {
                message = "Products retrieved successfully.",
                products
            });
        }


        /* ---------------------------- 
         * GET /api/Products/:id
         * ---------------------------- */
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Product>>> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return NotFound("Product not found");

            return Ok(new
            {
                message = "Product retrieved successfully.",
                product
            });
        }


        /* ---------------------------- 
         * POST /api/Products
         * ---------------------------- */
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(CreateProductDto Dto)
        {
            var supplierExists = await _context.Suppliers.AnyAsync(s => s.Id == Dto.SupplierId);

            if (!supplierExists)
                return BadRequest("Invalid SupplierId. Supplier does not exist.");

            var newProduct = new Product
            {
                Category = Dto.Category,
                Description = Dto.Description,
                Name = Dto.Name,
                Price = Dto.Price,
                StockCount = Dto.StockCount,
                SupplierId = Dto.SupplierId
            };

            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetProductById), 
                new { id = newProduct.Id }, 
                new {
                    message = "Product added successfully.",
                    newProduct
                });
        }


        /* ---------------------------- 
         * PUT /api/Products/:id
         * ---------------------------- */
        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, UpdateProductDto dto)
        {
            var supplierExists = await _context.Suppliers.AnyAsync(s => s.Id == dto.SupplierId);

            if (!supplierExists)
                return BadRequest("Invalid SupplierId. Supplier does not exist.");

            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return NotFound("Product not found");

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.Category = dto.Category;
            product.StockCount = dto.StockCount;
            product.UpdatedAt = DateTime.UtcNow;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Product updated successfully.",
                product
            });
        }


        /* ---------------------------- 
         * DELETE /api/Products/:id
         * ---------------------------- */
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return NotFound("Product not found");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Product deleted successfully."
            });
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryApp.Data;
using InventoryApp.Models;
using InventoryApp.DTO;

namespace InventoryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        // Simulate a database with an in-memory list of suppliers
        private List<Supplier> suppliers = SupplierStore.GetAllSuppliers();

        // Inject the Application DbContext using constructor to interact with the database
        private readonly AppDbContext _context;

        public SuppliersController(AppDbContext context)
        {
            _context = context;
        }


        /* ---------------------------- 
         * GET /api/Suppliers 
         * ---------------------------- */
        [HttpGet]
        public async Task<ActionResult<List<Supplier>>> GetSuppliers()
        {
            var suppliers = await _context.Suppliers.ToListAsync();
            return Ok(new
            {
                message = "Suppliers retrieved successfully.",
                suppliers
            });
        }


        /* ---------------------------- 
         * GET /api/Suppliers/{id}
         * ---------------------------- */
        [HttpGet("{id}")]
        public async Task<ActionResult<Supplier>> GetSupplierById(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);

            if (supplier == null)
                return BadRequest("Supplier not found.");

            return Ok(new
            {
                message = "Supplier retrieved successfully.",
                supplier
            });
        }


        /* ---------------------------- 
         * POST /api/Suppliers 
         * ---------------------------- */
        [HttpPost]
        public async Task<ActionResult<Supplier>> CreateSupplier(CreateSupplierDto dto)
        {
            var newSupplier = new Supplier
            {
                Firstname = dto.Firstname,
                Lastname = dto.Lastname,
                CompanyName = dto.CompanyName,
                Description = dto.Description,
                Founded = dto.Founded
            };

            _context.Suppliers.Add(newSupplier);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSupplierById), new { id = newSupplier.Id }, new
            {
                message = "Supplier created successfully.",
                newSupplier
            });
        }


        /* ---------------------------- 
         * PUT /api/suppliers/{id} 
         * ---------------------------- */
        [HttpPut("{id}")]
        public async Task<ActionResult<Supplier>> UpdateSupplier(int id, Supplier updatedSupplier)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            
            if (supplier == null) 
                return NotFound();

            supplier.Firstname = updatedSupplier.Firstname;
            supplier.Lastname = updatedSupplier.Lastname;
            supplier.CompanyName = updatedSupplier.CompanyName;
            supplier.Description = updatedSupplier.Description;
            supplier.Founded = updatedSupplier.Founded;
            supplier.UpdatedAt = DateTime.UtcNow;

            _context.Suppliers.Update(supplier);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Supplier updated successfully.",
                supplier
            });
        }


        /* ---------------------------- 
         * PATCH /api/suppliers/{id}
         * ---------------------------- */
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchSupplierDTO(int id, PatchSupplierDto dto)
        {
            var supplier = await _context.Suppliers.FindAsync(id);

            if (supplier == null)
                return NotFound();

            if (dto == null)
                return BadRequest();

            if (!string.IsNullOrWhiteSpace(dto.Firstname))
                supplier.Firstname = dto.Firstname;

            if (!string.IsNullOrWhiteSpace(dto.Lastname))
                supplier.Lastname = dto.Lastname;

            if (!string.IsNullOrWhiteSpace(dto.CompanyName))
                supplier.CompanyName = dto.CompanyName;

            if (!string.IsNullOrWhiteSpace(dto.Description))
                supplier.Description = dto.Description;

            if (dto.Founded != null)
                supplier.Founded = dto.Founded.Value;

            supplier.UpdatedAt = DateTime.UtcNow;

            _context.Suppliers.Update(supplier);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Supplier updated successfully",
                supplier
            });
        }


        /* ---------------------------- 
         * DELETE /api/suppliers/{id} 
         * ---------------------------- */
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);

            if (supplier == null) 
                return BadRequest();

            _context.Remove(supplier);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Supplier deleted successfully."
            });
        }
    }
}

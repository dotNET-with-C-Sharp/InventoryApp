using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InventoryApp.Data;
using InventoryApp.Models;
using InventoryApp.DTO;

namespace InventoryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private List<Supplier> suppliers = SupplierStore.GetAllSuppliers();

        [HttpGet]
        public ActionResult<List<Supplier>> GetSuppliers()
        {
            //var suppliers = SupplierStore.GetAllSuppliers();
            return Ok(suppliers);
        }


        [HttpGet("{id}")]
        public ActionResult<Supplier> GetSupplierById(int id)
        {
            var supplier = suppliers.SingleOrDefault(s => s.Id == id);

            if (supplier == null)
                return BadRequest("Supplier not found.");

            return Ok(supplier);
        }


        //[HttpPost]
        //public ActionResult<Supplier> CreateSupplier(Supplier newSupplier)
        //{
        //    if (newSupplier == null)
        //        return BadRequest("Supplier data is required.");

        //    if (string.IsNullOrWhiteSpace(newSupplier.Firstname))
        //        return BadRequest("Firstname is required.");

        //    if (string.IsNullOrWhiteSpace(newSupplier.CompanyName))
        //        return BadRequest("CompanyName is required.");

        //    suppliers.Add(newSupplier);

        //    return CreatedAtAction(nameof(GetSupplierById), new { Id = newSupplier.Id }, newSupplier);
        //}

        [HttpPost]
        public ActionResult<Supplier> CreateSupplier(CreateSupplierDTO dto)
        {
            int nextId = suppliers.Any() 
                ? suppliers.Max(s => s.Id) + 1 
                : suppliers.Count + 1;

            var newSupplier = new Supplier
            {
                //Id = suppliers.Count + 1,
                Id = nextId,
                Firstname = dto.Firstname,
                Lastname = dto.Lastname,
                CompanyName = dto.CompanyName,
                Description = dto.Description,
                Founded = dto.Founded
            };

            suppliers.Add(newSupplier);

            return CreatedAtAction(nameof(GetSupplierById), new { Id = newSupplier.Id }, newSupplier);
        }


        [HttpPut("{id}")]
        public ActionResult<Supplier> UpdateSupplier(int id, Supplier updatedSupplier)
        {
            if (id == 0)
                return BadRequest("Supplier Id is required.");

            var supplier = suppliers.FirstOrDefault(s => s.Id == id);
            if (supplier == null) return NotFound();

            //supplier.Id = supplier.Id;
            supplier.Firstname = updatedSupplier.Firstname;
            supplier.Lastname = updatedSupplier.Lastname;
            supplier.CompanyName = updatedSupplier.CompanyName;
            supplier.Description = updatedSupplier.Description;
            supplier.Founded = updatedSupplier.Founded;
            supplier.UpdatedAt = DateTime.UtcNow;

            //return NoContent();

            return Ok(new
            {
                message = "Supplier updated successfully.",
                supplier = supplier
            });
        }


        //[HttpPatch("{id}")]
        //public ActionResult<Supplier> PatchSupplier(int id, Supplier updatedSupplier)
        //{
        //    if (id == 0 || updatedSupplier == null) 
        //        return BadRequest("Supplier Id and updated supplier data is required");

        //    var supplier = suppliers.FirstOrDefault(s => s.Id == id); 
           
        //    if (supplier == null)
        //        return NotFound();

        //    if (!string.IsNullOrWhiteSpace(updatedSupplier.Firstname))
        //        supplier.Firstname = updatedSupplier.Firstname;

        //    if (!string.IsNullOrWhiteSpace(updatedSupplier.Lastname))
        //        supplier.Lastname= updatedSupplier.Lastname;

        //    if (!string.IsNullOrWhiteSpace(updatedSupplier.CompanyName))
        //        supplier.CompanyName = updatedSupplier.CompanyName;

        //    if (!string.IsNullOrWhiteSpace(updatedSupplier.Description))
        //        supplier.Description = updatedSupplier.Description;

        //    if (updatedSupplier.Founded != null)
        //        supplier.Founded = updatedSupplier.Founded.Value;

        //    supplier.UpdatedAt = DateTime.UtcNow;

        //    return Ok(new
        //    {
        //        message = "Supplier updated successfully",
        //        supplier
        //    });
        //}


        [HttpPatch("{id}")]
        public IActionResult PatchSupplierDTO(int id, PatchSupplierDto dto)
        {
            var supplier = suppliers.FirstOrDefault(s => s.Id == id);
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

            return Ok(new
            {
                message = "Supplier updated successfully",
                supplier
            });
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteSupplier(int id)
        {
            var supplier = suppliers.FirstOrDefault(s => s.Id == id);
            if (supplier == null) 
                return BadRequest();

            suppliers.Remove(supplier);

            return Ok(new
            {
                message = "Supplier deleted successfully."
            });
        }
    }
}

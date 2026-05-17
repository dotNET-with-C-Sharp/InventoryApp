using InventoryApp.Data;
using InventoryApp.DTO;
using InventoryApp.Interfaces;
using InventoryApp.Models;
using InventoryApp.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace InventoryApp.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly AppDbContext _context;
        private readonly IFileService _fileService;

        public SupplierService(AppDbContext appDbContext, IFileService fileService) {
            _context = appDbContext;
            _fileService = fileService;
        }

        public async Task<Supplier> CreateSupplierService(CreateSupplierDto dto)
        {
            string? imageUrl = null;

            if(dto.Image != null)
            {
                imageUrl = await _fileService.SaveImageAsync(dto.Image, "suppliers");
            }

            var newSupplier = new Supplier
            {
                Firstname = dto.Firstname,
                Lastname = dto.Lastname,
                CompanyName = dto.CompanyName,
                Description = dto.Description,
                Founded = dto.Founded,
                ImageUrl = imageUrl
            };

            _context.Suppliers.Add(newSupplier);
            await _context.SaveChangesAsync();

            return newSupplier;
        }
    }
}

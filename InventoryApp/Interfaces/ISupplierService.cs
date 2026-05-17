using InventoryApp.DTO;
using InventoryApp.Models;

namespace InventoryApp.Interfaces
{
    public interface ISupplierService
    {
        Task<Supplier> CreateSupplierService(CreateSupplierDto dto);
    }
}

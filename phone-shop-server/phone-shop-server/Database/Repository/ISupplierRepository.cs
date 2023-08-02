using phone_shop_server.Database.Entity;

namespace phone_shop_server.Database.Repository
{
    public interface ISupplierRepository
    {
        Task<bool> DeleteAsync(string id);
        Task<Supplier> GetOneAsync(string id);
        Task<IEnumerable<Supplier>> GetAllAsync();
        Task<Supplier> CreateAsync(Supplier supplier);
        Task<Supplier> UpdateAsync(Supplier supplier);

    }
}

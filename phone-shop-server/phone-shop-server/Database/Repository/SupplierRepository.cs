using Microsoft.EntityFrameworkCore;
using phone_shop_server.Database.Entity;

namespace phone_shop_server.Database.Repository
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly AppDbContext _appDbContext;
        public SupplierRepository(
            AppDbContext appDbContext
            )
        {
            _appDbContext = appDbContext;
        }
        public async Task<IEnumerable<Supplier>> GetAllAsync()
        {
            return await _appDbContext.Supplier.ToListAsync();
        }
        public async Task<Supplier> GetOneAsync(string id)
        {
            return await _appDbContext.Supplier
                .FirstOrDefaultAsync(dt => dt.Id.ToString().Equals(id));
        }
        public async Task<Supplier> CreateAsync(Supplier supplier)
        {
            _appDbContext.Supplier.Add(supplier);
            await _appDbContext.SaveChangesAsync();
            return supplier;
        }
        public async Task<Supplier> UpdateAsync(Supplier supplier)
        {
            _appDbContext.Supplier.Update(supplier);
            await _appDbContext.SaveChangesAsync();
            return supplier;
        }
        public async Task<bool> DeleteAsync(string id)
        {
            var supplier = await GetOneAsync(id);
            _appDbContext.Supplier.Remove(supplier);
            int result = await _appDbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}

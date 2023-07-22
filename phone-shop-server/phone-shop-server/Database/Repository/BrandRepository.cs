using Microsoft.EntityFrameworkCore;
using phone_shop_server.Database.Entity;

namespace phone_shop_server.Database.Repository
{
    public class BrandRepository : IBrandRepository
    {
        private readonly AppDbContext _appDbContext;
        public BrandRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IEnumerable<Brand>> GetAllAsync()
        {
            return await _appDbContext.Brand.ToListAsync();
        }
        public async Task<Brand> GetOneAsync(string id)
        {
            return await _appDbContext.Brand.FirstOrDefaultAsync(p => p.Id.ToString().Equals(id));
        }
        public async Task<Brand> CreateAsync(Brand brand)
        {
            _appDbContext.Brand.Add(brand);
            await _appDbContext.SaveChangesAsync();
            return brand;
        }
        public async Task<Brand> UpdateAsync(Brand brand)
        {
            _appDbContext.Update(brand);
            int i = await _appDbContext.SaveChangesAsync();
            return brand;
        }
        public async Task<bool> DeleteAsync(string id)
        {
            Brand brand = await GetOneAsync(id);
            _appDbContext.Brand.Remove(brand);
            int i = await _appDbContext.SaveChangesAsync();
            return i > 0;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using phone_shop_server.Database.Entity;

namespace phone_shop_server.Database.Repository
{
    public class DistrictRepository : IDistrictRepository
    {
        private readonly AppDbContext _appDbContext;
        public DistrictRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<District?> GetOneAsync(string? id)
        {
            return await _appDbContext.District.FirstOrDefaultAsync(h => h.Id.Equals(id));
        }
        public async Task<IEnumerable<District>?> GetByNameAsync(string? key)
        {
            return await _appDbContext.District.AsNoTracking().Where(h => h.Name.ToLower().Equals(key.ToLower())).ToListAsync();
        }
        public async Task<IEnumerable<District>?> GetByProvineIdAsync(string? id)
        {
            return await _appDbContext.District.AsNoTracking().Where(h => h.ProvinceId.Equals(id)).ToListAsync();
        }
    }
}

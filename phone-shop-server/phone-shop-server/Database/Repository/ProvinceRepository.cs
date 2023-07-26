using Microsoft.EntityFrameworkCore;
using phone_shop_server.Database.Entity;

namespace phone_shop_server.Database.Repository
{
    public class ProvinceRepository : IProvinceRepository
    {
        private readonly AppDbContext _appDbContext;
        public ProvinceRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IEnumerable<Province>> GetListProvine()
        {
            return await _appDbContext.Province.ToListAsync();
        }
        public async Task<Province?> FindByIdAsync(string? id)
        {
            return await _appDbContext.Province.FirstOrDefaultAsync(h => h.Id.Equals(id));
        }
        public async Task<IEnumerable<Province>?> FindByNameAsync(string? key)
        {
            return await _appDbContext.Province.Where(h => h.Name.ToLower().Equals(key.ToLower())).ToListAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using phone_shop_server.Database.Entity;

namespace phone_shop_server.Database.Repository
{
    public class HomeletRepository
    {
        private readonly AppDbContext _appDbContext;
        public HomeletRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }
        public async Task<Homelet?> GetOneAsync(string? id)
        {
            return await _appDbContext.Homelet.FirstOrDefaultAsync(h => h.Id.Equals(id));
        }
        public async Task<IEnumerable<Homelet>?> GetByNameAsync(string? key)
        {
            return await _appDbContext.Homelet.AsNoTracking().Where(h => h.Name.ToLower().Equals(key.ToLower())).ToListAsync();
        }
        public async Task<IEnumerable<Homelet>?> GetByDistrictIdAsync(string? id)
        {
            return await _appDbContext.Homelet.Where(h => h.DistrictId.Equals(id)).ToListAsync();
        }
    }
}

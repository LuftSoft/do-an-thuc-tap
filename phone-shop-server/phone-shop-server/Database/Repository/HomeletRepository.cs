using Microsoft.EntityFrameworkCore;
using phone_shop_server.Database.Entity;

namespace phone_shop_server.Database.Repository
{
    public class HomeletRepository : IHomeletRepository
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
        //Xa Nghia Trung, Huyen Tu Nghia, Tinh Quang Ngai
        public async Task<string> HomeletAddress(string homeletId)
        {
            var homelet = await _appDbContext.Homelet.FirstOrDefaultAsync(h=>h.Id.Equals(homeletId));
            var district = await _appDbContext.District.FirstOrDefaultAsync(d => d.Id.Equals(homelet.DistrictId));
            var province = await _appDbContext.Province.FirstOrDefaultAsync(p => p.Id.Equals(district.ProvinceId));
            return $"{homelet.Name}, {district.Name}, {province.Name}.";
        }
    }
}

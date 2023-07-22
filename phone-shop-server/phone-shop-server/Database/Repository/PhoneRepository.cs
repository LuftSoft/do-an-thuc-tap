using Microsoft.EntityFrameworkCore;
using phone_shop_server.Database.Entity;

namespace phone_shop_server.Database.Repository
{
    public class PhoneRepository : IPhoneRepository
    {
        private readonly AppDbContext _appDbContext;
        public PhoneRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IEnumerable<Phone>> GetAllAsync()
        {
            return await _appDbContext.Phone.ToListAsync();
        }
        public async Task<Phone> GetOneAsync(string id) 
        {
            return await _appDbContext.Phone.FirstOrDefaultAsync(p => p.Id.ToString().Equals(id));
        }
        public async Task<Phone> CreateAsync(Phone phone)
        {
            _appDbContext.Phone.Add(phone);
            await _appDbContext.SaveChangesAsync();
            return phone;
        }
        public async Task<Phone> UpdateAsync(Phone phone)
        {
            _appDbContext.Update(phone);
            await _appDbContext.SaveChangesAsync();
            return phone;
        }
        public async Task<bool> DeleteAsync(string id)
        {
            Phone phone = await GetOneAsync(id);
            _appDbContext.Phone.Remove(phone);
            var x = await _appDbContext.SaveChangesAsync();
            return x > 0;
        }
    }
}

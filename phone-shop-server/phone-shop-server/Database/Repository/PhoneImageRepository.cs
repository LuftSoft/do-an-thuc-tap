using Microsoft.EntityFrameworkCore;
using phone_shop_server.Business.Converter;
using phone_shop_server.Business.DTO.PhoneImage;
using phone_shop_server.Database.Entity;

namespace phone_shop_server.Database.Repository
{
    public class PhoneImageRepository : IPhoneImageRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPhoneImageConverter _phoneImageConverter;
        public PhoneImageRepository(
            AppDbContext appDbContext,
            IPhoneImageConverter phoneImageConverter
            )
        {
            _appDbContext = appDbContext;
            _phoneImageConverter = phoneImageConverter;
        }
        public async Task<PhoneImage> GetOneAsync(string id)
        {
            return await _appDbContext.PhoneImage.FirstOrDefaultAsync(p => p.Id.ToString().Equals(id));
        }
        public async Task<IEnumerable<PhoneImage>> GetByPhoneIdAsync(string id)
        { 
            return await _appDbContext.PhoneImage.Where(p => p.PhoneId.ToString().Equals(id)).ToListAsync();
        }
        public async Task<PhoneImage> CreateAsync(PhoneImage phoneImage)
        {
            _appDbContext.PhoneImage.Add(phoneImage);
            await _appDbContext.SaveChangesAsync();
            return phoneImage;
        }
        public async Task<bool> DeleteAsync(string id)
        {
            PhoneImage phoneImage = await _appDbContext.PhoneImage.FirstOrDefaultAsync(p => p.Id.ToString().Equals(id));
            _appDbContext.PhoneImage.Remove(phoneImage);
            int result = await _appDbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}

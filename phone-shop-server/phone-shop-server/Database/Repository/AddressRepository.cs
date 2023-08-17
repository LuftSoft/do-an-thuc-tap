using Microsoft.EntityFrameworkCore;
using phone_shop_server.Business.Converter;
using phone_shop_server.Business.DTO.Address;
using phone_shop_server.Database.Entity;

namespace phone_shop_server.Database.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IAddressConverter _addressConverter;
        public AddressRepository(
            AppDbContext appDbContext,
            IAddressConverter addressConverter
            )
        {
            _appDbContext = appDbContext;
            _addressConverter = addressConverter;
        }
        public async Task<AddressDto> CreateAsync(Address address)
        {
            if (address.isDefault)
            {
                var defaultAd = await _appDbContext.Address.FirstOrDefaultAsync(a => a.UserId == address.UserId && a.isDefault);
                if(defaultAd != null)
                {
                    defaultAd.isDefault = false;
                    await _appDbContext.SaveChangesAsync();
                }
            }
            _appDbContext.Address.Add( address );

            await _appDbContext.SaveChangesAsync();
            return await _addressConverter.ConvertAddressToAddressDto(address);
        }
        public async Task<AddressDto> UpdateAsync(Address address)
        {
            if(address.isDefault)
            {
                var listAddress = await _appDbContext.Address.Where(a => a.UserId == address.UserId).ToListAsync();
                foreach(var item in listAddress)
                {
                    if(item.Id != address.Id)
                    {
                        item.isDefault = false;
                        _appDbContext.Address.Update(item);
                        await _appDbContext.SaveChangesAsync();
                    }
                }
            }
            _appDbContext.Address.Update(address);
            await _appDbContext.SaveChangesAsync();
            return await _addressConverter.ConvertAddressToAddressDto(address);
        }
        public async Task<AddressDto> DeleteAsync(string id)
        {
            var address = await _appDbContext.Address.FirstOrDefaultAsync(a => a.Id.ToString().Equals(id));
            if (address == null)
                return null;
            _appDbContext.Address.Remove(address);
            await _appDbContext.SaveChangesAsync();
            return await _addressConverter.ConvertAddressToAddressDto(address);
        }
        public async Task<IEnumerable<AddressDto>> GetAllAsync()
        {
            var listAddress = await _appDbContext.Address.ToListAsync();
            return await _addressConverter.ConvertToListAddressDto(listAddress);
        }
        public async Task<AddressDto> GetOneAsync(string id)
        {
            var address = await _appDbContext.Address.FirstOrDefaultAsync(ad => ad.Id.ToString().Equals(id));
            return await _addressConverter.ConvertAddressToAddressDto(address);
        }
        public async Task<IEnumerable<AddressDto>> GetByUserIdAsync(string userId)
        {
           var listAddress = await _appDbContext.Address.Where(ad => ad.UserId.Equals(userId)).ToListAsync();
           return await _addressConverter.ConvertToListAddressDto(listAddress);
        }
    }
}

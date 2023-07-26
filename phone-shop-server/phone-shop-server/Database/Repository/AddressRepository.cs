using Microsoft.EntityFrameworkCore;
using phone_shop_server.Business.Converter;
using phone_shop_server.Business.DTO.Address;

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

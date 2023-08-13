using phone_shop_server.Business.DTO.Address;
using phone_shop_server.Database.Entity;

namespace phone_shop_server.Database.Repository
{
    public interface IAddressRepository
    {
        Task<IEnumerable<AddressDto>> GetAllAsync();
        Task<AddressDto> GetOneAsync(string id);
        Task<IEnumerable<AddressDto>> GetByUserIdAsync(string userId);
        Task<AddressDto> CreateAsync(Address address);
        Task<AddressDto> UpdateAsync(Address address);
        Task<AddressDto> DeleteAsync(string id);

    }
}

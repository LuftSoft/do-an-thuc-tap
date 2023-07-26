using phone_shop_server.Business.DTO.Address;

namespace phone_shop_server.Database.Repository
{
    public interface IAddressRepository
    {
        Task<IEnumerable<AddressDto>> GetAllAsync();
        Task<AddressDto> GetOneAsync(string id);
        Task<IEnumerable<AddressDto>> GetByUserIdAsync(string userId);

    }
}

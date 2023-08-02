using phone_shop_server.Business.DTO.Phone;

namespace phone_shop_server.Business.Service
{
    public interface IPhoneService
    {
        Task<List<PhoneDto>> GetAllAsync(int pageIndex, int pageSize, PhoneFilterDto? filterDto);
        Task<PhoneDto> GetOneAsync(string id);
        Task<PhoneDto> CreateAsync(PhoneCreateDto phoneCreateDto);
        Task<PhoneDto> UpdateAsync(PhoneUpdateDto phoneDto);
        Task<bool> UpdateQuantityAsync(string phoneId, int quantity);
        Task<bool> DeleteAsync(string id);

    }
}

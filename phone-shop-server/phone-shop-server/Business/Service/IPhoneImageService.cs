using phone_shop_server.Business.DTO.PhoneImage;

namespace phone_shop_server.Business.Service
{
    public interface IPhoneImageService
    {
        Task<PhoneImageDto> CreateAsync(PhoneImageDto phoneImageDto);
        Task<IEnumerable<PhoneImageDto>> GetByPhoneIdAsync(string phoneId);
    }
}

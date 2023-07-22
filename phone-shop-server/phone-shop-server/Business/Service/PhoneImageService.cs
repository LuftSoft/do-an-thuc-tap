using phone_shop_server.Business.Converter;
using phone_shop_server.Business.DTO.PhoneImage;
using phone_shop_server.Database.Entity;
using phone_shop_server.Database.Repository;

namespace phone_shop_server.Business.Service
{
    public class PhoneImageService : IPhoneImageService
    {
        private readonly IPhoneImageRepository _phoneImageRepository;
        private readonly IPhoneImageConverter _phoneImageConverter;
        public PhoneImageService(
            IPhoneImageRepository phoneImageRepository,
            IPhoneImageConverter phoneImageConverter
            ) 
        {
            _phoneImageRepository = phoneImageRepository;
            _phoneImageConverter = phoneImageConverter;
        }
        public async Task<PhoneImageDto> CreateAsync(PhoneImageDto phoneImageDto) 
        {
            PhoneImage phoneImage = _phoneImageConverter.ConvertToPhoneImage(phoneImageDto);
            var phoneImageCreate = await _phoneImageRepository.CreateAsync(phoneImage);
            return _phoneImageConverter.ConvertToPhoneImageDto(phoneImageCreate);
        }
        public async Task<IEnumerable<PhoneImageDto>> GetByPhoneIdAsync(string phoneId) 
        {
            var phoneImages = await _phoneImageRepository.GetByPhoneIdAsync(phoneId);
            return _phoneImageConverter.ConvertToListPhoneImageDto(phoneImages);
        }
    }
}

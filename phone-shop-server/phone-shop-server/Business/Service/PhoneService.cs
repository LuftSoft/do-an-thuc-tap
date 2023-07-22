using Humanizer;
using phone_shop_server.Business.Converter;
using phone_shop_server.Business.DTO.Phone;
using phone_shop_server.Business.DTO.PhoneImage;
using phone_shop_server.Database.Entity;
using phone_shop_server.Database.Repository;
using phone_shop_server.Util;

namespace phone_shop_server.Business.Service
{
    public class PhoneService : IPhoneService
    {
        private readonly IPhoneRepository _phoneRepository;
        private readonly IPhoneConverter _phoneConverter;
        private readonly IFileUtil _fileUtil;
        private readonly IPhoneImageService _phoneImageService;
        public PhoneService(
            IPhoneRepository phoneRepository,
            IPhoneConverter phoneConverter,
            IFileUtil fileUtil,
            IPhoneImageService phoneImageService
            )
        {
            _phoneRepository = phoneRepository;
            _phoneConverter = phoneConverter;
            _fileUtil = fileUtil;
            _phoneImageService = phoneImageService;
        }
        public async Task<List<PhoneDto>> GetAllAsync(int pageIndex, int pageSize, PhoneFilterDto? filterDto)
        {
            List<Phone> phones = (await _phoneRepository.GetAllAsync()).ToList();
            if(filterDto != null)
            {
                //filter dto here
            }
            phones = phones.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            return (await _phoneConverter.ConvertToListPhoneDto(phones)).ToList();
        }
        public async Task<PhoneDto> GetOneAsync(string id)
        {
            Phone phone = await _phoneRepository.GetOneAsync(id);
            return await _phoneConverter.ConvertToPhoneDto(phone);
        }
        public async Task<PhoneDto> CreateAsync(PhoneCreateDto phoneCreateDto) 
        {
            Phone phone = _phoneConverter.ConvertPhoneCreateDtoToPhone(phoneCreateDto);
            Phone createPhone = await _phoneRepository.CreateAsync(phone);
            List<string> phoneImages = (await _fileUtil.MultiUploadAsync(phoneCreateDto.PhoneImages.ToArray())).ToList();
            foreach(var phoneImageLink in phoneImages){
                await _phoneImageService.CreateAsync(new PhoneImageDto()
                {
                    Link = phoneImageLink,
                    PhoneId = createPhone.Id.ToString()
                });
            }
            return await _phoneConverter.ConvertToPhoneDto(createPhone);
        }
        public async Task<PhoneDto> UpdateAsync(PhoneUpdateDto phoneDto)
        {
            Phone phone = _phoneConverter.ConvertPhoneUpdateDtoToPhone(phoneDto);
            Phone updatePhone = await _phoneRepository.UpdateAsync(phone);
            return await _phoneConverter.ConvertToPhoneDto(updatePhone);
        }
        public async Task<bool> DeleteAsync(string id)
        {
            return await _phoneRepository.DeleteAsync(id);
        }
    }
}

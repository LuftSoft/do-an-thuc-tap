using phone_shop_server.Business.DTO.Phone;
using phone_shop_server.Business.Service;
using phone_shop_server.Database.Entity;
using phone_shop_server.Util;
using System.Reflection.Metadata;

namespace phone_shop_server.Business.Converter
{
    public interface IPhoneConverter
    {
        Task<PhoneDto> ConvertToPhoneDto(Phone phone);
        Task<IEnumerable<PhoneDto>> ConvertToListPhoneDto(IEnumerable<Phone> phone);
        Phone ConvertPhoneCreateDtoToPhone(PhoneCreateDto phoneCreateDto);
        Phone ConvertPhoneDtoToPhone(PhoneDto phoneDto);
        Phone ConvertPhoneUpdateDtoToPhone(PhoneUpdateDto phoneUpdateDto);

    }
    public class PhoneConverter : IPhoneConverter
    {
        private readonly IPhoneImageService _phoneImageService;
        private readonly IBrandService _brandService;
        public PhoneConverter(
            IBrandService brandService,
            IPhoneImageService phoneImageService
            )
        {
            _brandService = brandService;
            _phoneImageService = phoneImageService;
        }
        public Phone ConvertPhoneCreateDtoToPhone(PhoneCreateDto phoneCreateDto)
        {
            Phone phone = new Phone();
            phone.Name = phoneCreateDto.Name;
            phone.CPU = phoneCreateDto.CPU;
            phone.PIN = phoneCreateDto.PIN;
            phone.ROM = phoneCreateDto.ROM;
            phone.RAM = phoneCreateDto.RAM;
            phone.BehindCamera = phoneCreateDto.BehindCamera;
            phone.FrontCamera = phoneCreateDto.FrontCamera;
            phone.BrandId = Guid.Parse(phoneCreateDto.BrandId.ToString());
            phone.Description = phoneCreateDto.Description;
            phone.Operation = phoneCreateDto.Operation;
            phone.OtherBenefit = phoneCreateDto.OtherBenefit;
            phone.ScreenResolution = phoneCreateDto.ScreenResolution;
            phone.ScreenSize = phoneCreateDto.ScreenSize;
            phone.ScreenTouch = phoneCreateDto.ScreenTouch;
            phone.ImportPrice = phoneCreateDto.ImportPrice;
            phone.SoldPrice = phoneCreateDto.SoldPrice;
            phone.Slug = phoneCreateDto.Slug;
            return phone;
        }
        public Phone ConvertPhoneUpdateDtoToPhone(PhoneUpdateDto phoneUpdateDto)
        {
            Phone phone = new Phone();
            phone.Id = Guid.Parse(phoneUpdateDto.Id);
            phone.Name = phoneUpdateDto.Name;
            phone.CPU = phoneUpdateDto.CPU;
            phone.PIN = phoneUpdateDto.PIN;
            phone.ROM = phoneUpdateDto.ROM;
            phone.RAM = phoneUpdateDto.RAM;
            phone.BehindCamera = phoneUpdateDto.BehindCamera;
            phone.FrontCamera = phoneUpdateDto.FrontCamera;
            phone.BrandId = Guid.Parse(phoneUpdateDto.BrandId.ToString());
            phone.Description = phoneUpdateDto.Description;
            phone.Operation = phoneUpdateDto.Operation;
            phone.OtherBenefit = phoneUpdateDto.OtherBenefit;
            phone.ScreenResolution = phoneUpdateDto.ScreenResolution;
            phone.ScreenSize = (double)phoneUpdateDto.ScreenSize;
            phone.ScreenTouch = phoneUpdateDto.ScreenTouch;
            phone.ImportPrice = phoneUpdateDto.ImportPrice;
            phone.SoldPrice = phoneUpdateDto.SoldPrice;
            phone.Slug = phoneUpdateDto.Slug;
            return phone;
        }
        public async Task<PhoneDto> ConvertToPhoneDto(Phone phone)
        {
            var phoneImages = await _phoneImageService.GetByPhoneIdAsync(phone.Id.ToString());
            var brand = await _brandService.GetOneAsync(phone.BrandId.ToString());
            PhoneDto phoneDto = new PhoneDto();
            phoneDto.PhoneImages = phoneImages.ToList();
            phoneDto.Id = phone.Id.ToString();
            phoneDto.Name = phone.Name;
            phoneDto.CPU = phone.CPU;
            phoneDto.PIN = phone.PIN;
            phoneDto.ROM = phone.ROM;
            phoneDto.RAM = phone.RAM;
            phoneDto.BehindCamera = phone.BehindCamera;
            phoneDto.FrontCamera = phone.FrontCamera;
            phoneDto.Brand = brand;
            phoneDto.Description = phone.Description;
            phoneDto.OtherBenefit = phone.OtherBenefit;
            phoneDto.Operation = phone.Operation;
            phoneDto.ScreenResolution = phone.ScreenResolution;
            phoneDto.ScreenSize = phone.ScreenSize;
            phoneDto.ScreenTouch = phone.ScreenTouch;
            phoneDto.Quantity = phone.Quantity;
            phoneDto.ImportPrice = phone.ImportPrice;
            phoneDto.SoldPrice = phone.SoldPrice;
            phoneDto.Slug = phone.Slug;
            return phoneDto;
        }
        public Phone ConvertPhoneDtoToPhone(PhoneDto phoneDto)
        {
            Phone phone = new Phone();
            phone.Id = Guid.Parse(phoneDto.Id) ;
            phone.Name = phoneDto.Name ;
            phone.CPU = phoneDto.CPU ;
            phone.PIN = phoneDto.PIN ;
            phone.ROM = phoneDto.ROM ;
            phone.RAM = phoneDto.RAM ;
            phone.BehindCamera = phoneDto.BehindCamera ;
            phone.FrontCamera = phoneDto.FrontCamera ;
            phone.BrandId = Guid.Parse(phoneDto.Brand.Id) ;
            phone.Description = phoneDto.Description ;
            phone.Operation = phoneDto.Operation ;
            phone.OtherBenefit = phoneDto.OtherBenefit;
            phone.ScreenResolution = phoneDto.ScreenResolution ;
            phone.ScreenSize = (double)phoneDto.ScreenSize ;
            phone.ScreenTouch = phoneDto.ScreenTouch ;
            phone.Quantity = (int)phoneDto.Quantity ;
            phone.ImportPrice = phoneDto.ImportPrice ;
            phone.SoldPrice = phoneDto.SoldPrice ;
            phone.Slug = phoneDto.Slug;
            return phone;
        }
        public async Task<IEnumerable<PhoneDto>> ConvertToListPhoneDto(IEnumerable<Phone> phones)
        {
            List<PhoneDto> phoneDtos = new List<PhoneDto>();
            foreach(var p in phones)
            {
                phoneDtos.Add(await ConvertToPhoneDto(p));
            }
            return phoneDtos;
        }
    }
}

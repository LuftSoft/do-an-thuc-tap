using phone_shop_server.Business.DTO.PhoneImage;
using phone_shop_server.Database.Entity;

namespace phone_shop_server.Business.Converter
{
    public interface IPhoneImageConverter
    {
        PhoneImage ConvertToPhoneImage(PhoneImageDto phoneImageDto);
        List<PhoneImage> ConvertToListPhoneImage(IEnumerable<PhoneImageDto> phoneImageDtos);
        PhoneImageDto ConvertToPhoneImageDto(PhoneImage phoneImage);
        List<PhoneImageDto> ConvertToListPhoneImageDto(IEnumerable<PhoneImage> phoneImages);
    }
    public class PhoneImageConverter : IPhoneImageConverter
    {
        public PhoneImageDto ConvertToPhoneImageDto(PhoneImage phoneImage)
        {
            return new PhoneImageDto()
            {
                Id = phoneImage.Id.ToString(),
                Link = phoneImage.Link,
                PhoneId = phoneImage.PhoneId.ToString()
            };
        }
        public List<PhoneImageDto> ConvertToListPhoneImageDto(IEnumerable<PhoneImage> phoneImages)
        {
            List<PhoneImageDto> phoneImageDtos = new List<PhoneImageDto>();
            phoneImages.ToList().ForEach(phoneImage => 
            {
                phoneImageDtos.Add(ConvertToPhoneImageDto(phoneImage));
            });
            return phoneImageDtos;
        }

        public PhoneImage ConvertToPhoneImage(PhoneImageDto phoneImage)
        {
            return new PhoneImage()
            {
                Link = phoneImage.Link,
                PhoneId = Guid.Parse(phoneImage.PhoneId)
            };
        }

        public List<PhoneImage> ConvertToListPhoneImage(IEnumerable<PhoneImageDto> phoneImageDto)
        {
            List<PhoneImage> phoneImages = new List<PhoneImage>();
            phoneImageDto.ToList().ForEach(phoneImage =>
            {
                phoneImages.Add(ConvertToPhoneImage(phoneImage));
            });
            return phoneImages;
        }
    }
}

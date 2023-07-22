using phone_shop_server.Business.DTO.Brand;
using phone_shop_server.Database.Entity;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace phone_shop_server.Business.Converter
{
    public interface IBrandConverter
    {
        BrandDto ConvertBrandToBrandDto(Brand brand);
        Brand ConvertBrandDtoToBrand(BrandDto brandDto);

    }
    public class BrandConverter : IBrandConverter
    {
        public BrandDto ConvertBrandToBrandDto(Brand brand)
        {
            return new BrandDto()
            {
                Id = brand.Id.ToString(),
                Name = brand.Name,
                Description = brand.Description,
                Logo = brand.Logo,
            };
        }
        public Brand ConvertBrandDtoToBrand(BrandDto brandDto)
        {
            return new Brand()
            {
                Id = Guid.Parse(brandDto.Id),
                Name = brandDto.Name,
                Description = brandDto.Description,
                Logo = brandDto.Logo,
            };
        }
    }
}

using phone_shop_server.Database.Entity;
using phone_shop_server.Database;
using phone_shop_server.Database.Repository;
using phone_shop_server.Business.DTO.Brand;
using phone_shop_server.Util;
using phone_shop_server.Business.Converter;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace phone_shop_server.Business.Service
{
    public interface IBrandService
    {
        Task<IEnumerable<Brand>> GetAllAsync();
        Task<BrandDto> GetOneAsync(string id);
        Task<BrandDto> CreateAsync(BrandCreateDto brandCreateDto);
        Task<BrandDto> UpdateAsync(BrandDto dto);
        Task<bool> DeleteAsync(string id);

    }
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IFileUtil _fileUtil;
        private readonly IBrandConverter _brandConverter;
        public BrandService(
            IBrandRepository brandRepository,
            IFileUtil fileUtil,
            IBrandConverter brandConverter
            )
        {
            _brandRepository = brandRepository;
            _fileUtil = fileUtil;
            _brandConverter = brandConverter;
        }
        public async Task<IEnumerable<Brand>> GetAllAsync()
        {
            return await _brandRepository.GetAllAsync();
        }
        public async Task<BrandDto> GetOneAsync(string id)
        {
            return _brandConverter.ConvertBrandToBrandDto(await _brandRepository.GetOneAsync(id));
        }
        public async Task<BrandDto> CreateAsync(BrandCreateDto brandCreateDto)
        {
            string brandLogo = await _fileUtil.UploadAsync(brandCreateDto.Logo);
            Brand brand = await _brandRepository.CreateAsync(new Brand()
            {
                Id = new Guid(),
                Name = brandCreateDto.Name,
                Description = brandCreateDto.Description,
                Logo = brandLogo,
            });
            return _brandConverter.ConvertBrandToBrandDto(brand);
        }
        public async Task<BrandDto> UpdateAsync(BrandDto dto)
        {
            Brand brand = _brandConverter.ConvertBrandDtoToBrand(dto);
            await _brandRepository.UpdateAsync(brand);
            return _brandConverter.ConvertBrandToBrandDto(brand);
        }
        public async Task<bool> DeleteAsync(string id)
        {
            bool result = await _brandRepository.DeleteAsync(id);
            return result;
        }
    }
}

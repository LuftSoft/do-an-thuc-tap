using phone_shop_server.Business.DTO.Supplier;
using phone_shop_server.Database.Entity;
using phone_shop_server.Database.Repository;

namespace phone_shop_server.Business.Converter
{
    public interface ISupplierConverter
    {
        Task<SupplierDto> ConvertToSuppilerDto(Supplier supplier);
        Supplier MapSupplierDtoToSupplierEntity(SupplierDto supplierDto);
        Supplier MapToSupplierEntity(SupplierCreateDto supplierCreateDto);
        Task<IEnumerable<SupplierDto>> ConvertToListSuppilerDto(IEnumerable<Supplier> suppliers);

    }
    public class SupplierConverter : ISupplierConverter
    {
        private readonly IHomeletRepository _homeletRepository;
        public SupplierConverter
            (
            IHomeletRepository homeletRepository
            )
        {
            _homeletRepository = homeletRepository;
        }
        public Supplier MapToSupplierEntity(SupplierCreateDto supplierCreateDto)
        {
            return new Supplier()
            {
                Name = supplierCreateDto.Name,
                HomeletId = supplierCreateDto.HomeletId,
                Description = supplierCreateDto.Description,
            };
        }
        public Supplier MapSupplierDtoToSupplierEntity(SupplierDto supplierDto)
        {
            return new Supplier()
            {
                Id = supplierDto.Id,
                Name = supplierDto.Name,
                HomeletId = supplierDto.HomeletId,
                Description = supplierDto.Description,
            };
        }
        public async Task<SupplierDto> ConvertToSuppilerDto(Supplier supplier)
        {
            var detailLocation = await _homeletRepository.GetHomeletAddress(supplier.HomeletId);
            return new SupplierDto()
            {
                Id = supplier.Id,
                Name = supplier.Name,
                Description = supplier.Description,
                HomeletId = supplier.HomeletId,
                DetailLocation = detailLocation
            };
        }
        public async Task<IEnumerable<SupplierDto>> ConvertToListSuppilerDto(IEnumerable<Supplier> suppliers)
        {
            List<SupplierDto> result = new List<SupplierDto>();
            foreach(var supplier in suppliers)
            {
                result.Add(await ConvertToSuppilerDto(supplier));
            }
            return result;
        }
    }
}

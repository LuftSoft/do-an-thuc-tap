using phone_shop_server.Business.Converter;
using phone_shop_server.Business.DTO.Supplier;
using phone_shop_server.Database.Entity;
using phone_shop_server.Database.Enum;
using phone_shop_server.Database.Repository;

namespace phone_shop_server.Business.Service
{
    public interface ISupplierService
    {
        Task<APIResponse.APIResponse> GetAllAsync();
        Task<APIResponse.APIResponse> DeleteAsync(string id);
        Task<APIResponse.APIResponse> GetOneAsync(string supplierId);
        Task<APIResponse.APIResponse> UpdateAsync(SupplierDto supplierDto);
        Task<APIResponse.APIResponse> CreateAsync(SupplierCreateDto supplierDto);

    }
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierConverter _supplierConverter;
        private readonly IHomeletRepository _homeletRepository;
        private readonly ISupplierRepository _supplierRepository;
        public SupplierService(
            ISupplierConverter supplierConverter,
            IHomeletRepository homeletRepository,
            ISupplierRepository supplierRepository
            )
        {
            _supplierConverter = supplierConverter;
            _homeletRepository = homeletRepository;
            _supplierRepository = supplierRepository;
        }
        public async Task<APIResponse.APIResponse> GetAllAsync()
        {
            try
            {
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.ERROR.ToString(),
                    data = await _supplierConverter.ConvertToListSuppilerDto(await _supplierRepository.GetAllAsync())
                };
            }
            catch (Exception ex)
            {
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.ERROR.ToString(),
                    message = ex.Message
                };
                throw new ApplicationException(ex.InnerException.ToString());
            }

        }
        public async Task<APIResponse.APIResponse> GetOneAsync(string supplierId)
        {
            try
            {
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.ERROR.ToString(),
                    data = await _supplierConverter.ConvertToSuppilerDto(await _supplierRepository.GetOneAsync(supplierId))
                };
            }
            catch (Exception ex)
            {
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.ERROR.ToString(),
                    message = ex.Message
                };
                throw new ApplicationException(ex.InnerException.ToString());
            }
        }
        public async Task<APIResponse.APIResponse> CreateAsync(SupplierCreateDto supplierDto)
        {
            try
            {
                var homelet = await _homeletRepository.GetOneAsync(supplierDto.HomeletId);
                if (homelet == null)
                {
                    return new APIResponse.APIResponse()
                    {
                        code = StatusCode.ERROR.ToString(),
                        message = "homelet is null"
                    };
                }
                var supplier = await _supplierRepository
                    .CreateAsync(_supplierConverter.MapToSupplierEntity(supplierDto));
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.SUCCESS.ToString(),
                    data = await _supplierConverter.ConvertToSuppilerDto(supplier)
                };
            }
            catch (Exception ex)
            {
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.ERROR.ToString(),
                    message = ex.Message
                };
                throw new ApplicationException(ex.InnerException.ToString());
            }
        }
        public async Task<APIResponse.APIResponse> UpdateAsync(SupplierDto supplierDto)
        {
            try
            {
                var homelet = await _homeletRepository.GetOneAsync(supplierDto.HomeletId);
                if (homelet == null)
                {
                    return new APIResponse.APIResponse()
                    {
                        code = StatusCode.ERROR.ToString(),
                        message = "homelet is null"
                    };
                }
                var supplier = await _supplierRepository
                    .UpdateAsync(_supplierConverter.MapSupplierDtoToSupplierEntity(supplierDto));
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.SUCCESS.ToString(),
                    data = await _supplierConverter.ConvertToSuppilerDto(supplier)
                };
            }
            catch (Exception ex)
            {
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.ERROR.ToString(),
                    message = ex.Message
                };
                throw new ApplicationException(ex.InnerException.ToString());
            }
        }
        public async Task<APIResponse.APIResponse> DeleteAsync(string id)
        {
            try
            {
                var result = await _supplierRepository.DeleteAsync(id);
                if (!result)
                {
                    return new APIResponse.APIResponse()
                    {
                        code = StatusCode.ERROR.ToString(),
                        message = "delete supplier failed"
                    };
                }
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.SUCCESS.ToString()
                };
            }
            catch (Exception ex)
            {
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.ERROR.ToString(),
                    message = ex.Message
                };
                throw new ApplicationException(ex.InnerException.ToString());
            }
        }

    }
}

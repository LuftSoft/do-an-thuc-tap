using phone_shop_server.Database.Entity;
using phone_shop_server.Database;
using phone_shop_server.Database.Repository;
using phone_shop_server.Business.Converter;
using phone_shop_server.Database.Enum;
using phone_shop_server.Business.DTO.Warehouse;
using System.Transactions;

namespace phone_shop_server.Business.Service
{
    public interface IWarehouseTicketService
    {
        Task<APIResponse.APIResponse> GetAllAsync();
        Task<APIResponse.APIResponse> GetOneAsync(string id);
        Task<APIResponse.APIResponse> CreateAsync(WarehouseTicketCreateDto warehouseCreateTicket);
        Task<APIResponse.APIResponse> UpdateAsync(WarehouseTicketCreateDto warehouseTicketDto);
        Task<APIResponse.APIResponse> DeleteAsync(string id);

    }
    public class WarehouseTicketService : IWarehouseTicketService
    {
        private readonly ITicketDetailRepository _ticketDetailRepository;
        private readonly IWarehouseTicketConverter _warehouseTicketConverter;
        private readonly IWarehouseTicketRepository _warehouseTicketRepository;
        private readonly IPhoneService _phoneService;
        public WarehouseTicketService(
            IPhoneService phoneService,
            ITicketDetailRepository ticketDetailRepository,
            IWarehouseTicketConverter warehouseTicketConverter,
            IWarehouseTicketRepository warehouseTicketRepository
            )
        {
            _phoneService = phoneService;
            _ticketDetailRepository = ticketDetailRepository;
            _warehouseTicketConverter = warehouseTicketConverter;
            _warehouseTicketRepository = warehouseTicketRepository;
        }

        public async Task<APIResponse.APIResponse> GetAllAsync()
        {
            try
            {
                var warehouseTickets = await _warehouseTicketRepository.GetAllAsync();
                if (warehouseTickets == null)
                {
                    return new APIResponse.APIResponse()
                    {
                        code = StatusCode.ERROR.ToString(),
                        message = "Warehouse ticket is null"
                    };
                }
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.SUCCESS.ToString(),
                    data = await _warehouseTicketConverter.ConvertToListWarehouseTicketDto(warehouseTickets)
                };
            }
            catch (Exception ex)
            {
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.ERROR.ToString(),
                    message = ex.Message,
                };
                throw new ApplicationException(ex.InnerException.ToString());
            }
        }
        public async Task<APIResponse.APIResponse> GetOneAsync(string id)
        {
            try
            {
                var warehouseTicket = await _warehouseTicketRepository.GetOneAsync(id);
                if (warehouseTicket == null)
                {
                    return new APIResponse.APIResponse()
                    {
                        code = StatusCode.ERROR.ToString(),
                        message = "Warehouse ticket is null"
                    };
                }
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.SUCCESS.ToString(),
                    data = await _warehouseTicketConverter.ConvertToWarehouseTicketDto(warehouseTicket)
                };
            }
            catch (Exception ex)
            {
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.ERROR.ToString(),
                    message = ex.Message,
                };
                throw new ApplicationException(ex.InnerException.ToString());
            }
        }
        public async Task<APIResponse.APIResponse> CreateAsync(WarehouseTicketCreateDto warehouseCreateTicket)
        {
            try
            {
                    var warehouseConvert = _warehouseTicketConverter.MapToWarehouseTicketEntity(warehouseCreateTicket);
                    var warehouseTicket = await _warehouseTicketRepository.CreateAsync(warehouseConvert);
                    if (warehouseTicket == null)
                    {
                        return new APIResponse.APIResponse()
                        {
                            code = StatusCode.ERROR.ToString(),
                            message = "Can't create warehouse ticket"
                        };
                    }
                    foreach (var phone in warehouseCreateTicket.phones)
                    {
                        await _ticketDetailRepository
                            .CreateAsync(_warehouseTicketConverter
                            .MapToTicketDetailEntity(phone, warehouseTicket));
                        await _phoneService.UpdateQuantityAsync(phone.phoneId, phone.quantity);
                    }
                    return new APIResponse.APIResponse()
                    {
                        code = StatusCode.SUCCESS.ToString(),
                        data = await _warehouseTicketConverter.ConvertToWarehouseTicketDto(warehouseTicket)
                    };
            }
            catch (Exception ex)
            {
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.ERROR.ToString(),
                    message = ex.Message,
                };
                throw new ApplicationException(ex.InnerException.ToString());
            }
        }
        public async Task<APIResponse.APIResponse> UpdateAsync(WarehouseTicketCreateDto warehouseTicketDto)
        {
            try
            {
                var warehouseConvert = _warehouseTicketConverter.MapToWarehouseTicketEntity(warehouseTicketDto);
                var warehouseTicket = await _warehouseTicketRepository.UpdateAsync(warehouseConvert);
                if (warehouseTicket == null)
                {
                    return new APIResponse.APIResponse()
                    {
                        code = StatusCode.ERROR.ToString(),
                        message = "Can't create warehouse ticket"
                    };
                }
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.SUCCESS.ToString(),
                    data = await _warehouseTicketConverter.ConvertToWarehouseTicketDto(warehouseTicket)
                };
            }
            catch (Exception ex)
            {
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.ERROR.ToString(),
                    message = ex.Message,
                };
                throw new ApplicationException(ex.InnerException.ToString());
            }
        }
        public async Task<APIResponse.APIResponse> DeleteAsync(string id)
        {
            try
            {
                var warehouseTicket = await _warehouseTicketRepository.GetOneAsync(id);
                if (warehouseTicket == null)
                {
                    return new APIResponse.APIResponse()
                    {
                        code = StatusCode.ERROR.ToString(),
                        message = "Can't delete warehouse ticket"
                    };
                }
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.SUCCESS.ToString(),
                    data = await _warehouseTicketRepository.DeleteAsync(id)
                };
            }
            catch (Exception ex)
            {
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.ERROR.ToString(),
                    message = ex.Message,
                };
                throw new ApplicationException(ex.InnerException.ToString());
            }
        }
    }
}

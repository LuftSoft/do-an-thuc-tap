using phone_shop_server.Business.DTO.Order;
using phone_shop_server.Business.DTO.Warehouse;
using phone_shop_server.Database.Entity;
using phone_shop_server.Database.Repository;

namespace phone_shop_server.Business.Converter
{
    public interface IWarehouseTicketConverter
    {
        Task<TicketDetailDto> ConvertToTicketDetailDto(TicketDetail ticketDetail);
        Task<WarehouseTicketDto> ConvertToWarehouseTicketDto(WarehouseTicket warehouseTicket);
        WarehouseTicket MapToWarehouseTicketEntity(WarehouseTicketCreateDto warehouseTicket);
        Task<IEnumerable<TicketDetailDto>> ConvertToTicketDetailDto(IEnumerable<TicketDetail> ticketDetails);
        TicketDetail MapToTicketDetailEntity(OrderCreatePhoneDto phoneCreateDto, WarehouseTicket warehouseTicket);
        Task<IEnumerable<WarehouseTicketDto>> ConvertToListWarehouseTicketDto(IEnumerable<WarehouseTicket> warehouseTickets);


    }
    public class WarehouseTicketConverter : IWarehouseTicketConverter
    {
        private readonly IOrderConverter _orderConverter;
        private readonly IPhoneConverter _phoneConverter;
        private readonly IPhoneRepository _phoneRepository;
        private readonly ISupplierConverter _supplierConverter;
        private readonly ISupplierRepository _supplierRepository;
        private readonly ITicketDetailRepository _ticketDetailRepository;
        public WarehouseTicketConverter(
            IOrderConverter orderConverter, 
            IPhoneConverter phoneConverter, 
            IPhoneRepository phoneRepository, 
            ISupplierConverter supplierConverter, 
            ISupplierRepository supplierRepository, 
            ITicketDetailRepository ticketDetailRepository
            )
        {
            _orderConverter = orderConverter;
            _phoneConverter = phoneConverter;
            _phoneRepository = phoneRepository;
            _supplierConverter = supplierConverter;
            _supplierRepository = supplierRepository;
            _ticketDetailRepository = ticketDetailRepository;
        }

        public WarehouseTicket MapToWarehouseTicketEntity(WarehouseTicketCreateDto warehouseTicket)
        {
            return new WarehouseTicket()
            {
                Created =(DateTime) warehouseTicket.Created,
                SupplierId = Guid.Parse(warehouseTicket.SupplierId),
                UserId = warehouseTicket.UserId
            };
        }
        public TicketDetail MapToTicketDetailEntity(OrderCreatePhoneDto phoneCreateDto, WarehouseTicket warehouseTicket)
        {
            return new TicketDetail()
            {
                Quantity = phoneCreateDto.quantity,
                WarehouseTicketId = warehouseTicket.Id,
                PhoneId = Guid.Parse(phoneCreateDto.phoneId),
            };
        }
        public async Task<TicketDetailDto> ConvertToTicketDetailDto(TicketDetail ticketDetail)
        {
            var phone = await _phoneRepository.GetOneAsync(ticketDetail.PhoneId.ToString());
            return new TicketDetailDto()
            {
                Id = ticketDetail.Id.ToString(),
                PhoneId = ticketDetail.PhoneId.ToString(),
                Quantity = ticketDetail.Quantity,
                WarehouseTicketId = ticketDetail.WarehouseTicketId.ToString(),
                PhoneDto = await _phoneConverter.ConvertToPhoneDto(phone)
            };
        }
        public async Task<WarehouseTicketDto> ConvertToWarehouseTicketDto(WarehouseTicket warehouseTicket)
        {
            var supplier = await _supplierConverter
                .ConvertToSuppilerDto(await _supplierRepository.GetOneAsync(warehouseTicket.SupplierId.ToString()));
            var detailTickets = await _ticketDetailRepository.GetByWarehouseTicketIdAsync(warehouseTicket.Id.ToString());
            return new WarehouseTicketDto()
            {
                Id = warehouseTicket.Id.ToString(),
                Created = warehouseTicket.Created,
                SupplierId = warehouseTicket.SupplierId.ToString(),
                supplierDto = supplier,
                UserId = warehouseTicket.UserId,
                ticketDetailDtos = await ConvertToTicketDetailDto(detailTickets)
            };
        }
        public async Task<IEnumerable<TicketDetailDto>> ConvertToTicketDetailDto(IEnumerable<TicketDetail> ticketDetails)
        {
            List<TicketDetailDto> ticketDetailDtos = new List<TicketDetailDto>();
            foreach (var ticketDetail in ticketDetails)
            {
                ticketDetailDtos.Add(await ConvertToTicketDetailDto(ticketDetail));
            }
            return ticketDetailDtos;
        }
        public async Task<IEnumerable<WarehouseTicketDto>> ConvertToListWarehouseTicketDto(IEnumerable<WarehouseTicket> warehouseTickets)
        {
            List<WarehouseTicketDto> warehouseTicketDtos = new List<WarehouseTicketDto>();
            foreach(var warehouseTicket in warehouseTickets)
            {
                warehouseTicketDtos.Add(await ConvertToWarehouseTicketDto(warehouseTicket));
            }
            return warehouseTicketDtos;
        }
    }
}

using phone_shop_server.Business.DTO.Order;
using phone_shop_server.Database.Entity;
using phone_shop_server.Database.Repository;

namespace phone_shop_server.Business.Converter
{
    public interface IOrderConverter
    {
        StatusDto ConvertToStatusDto(Status status);
        Task<OrderDto> ConvertToOrderDto(Order order);
        OrderStatus ConvertDtoToOrderStatus(OrderStatusDto dto);
        Task<OrderStatusDto> ConvertToOrderStatusDto(OrderStatus dto);
        Task<OrderDetailDto> ConvertToOrderDetailDto(OrderDetail orderDetail);
        Task<IEnumerable<OrderDto>> ConvertToListOrderDto(IEnumerable<Order> orders);
        Task<IEnumerable<OrderStatusDto>> ConvertToListOrderStatusDto(IEnumerable<OrderStatus> orderStatuses);
        Task<IEnumerable<OrderDetailDto>> ConvertToListOrderDetailDto(IEnumerable<OrderDetail> orderDetails);

    }
    public class OrderConverter : IOrderConverter
    {
        private readonly IPhoneConverter _phoneConverter;
        private readonly IPhoneRepository _phoneRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IAddressConverter _addressConverter;
        private readonly IAddressRepository _addressRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IOrderStatusRepository _orderStatusRepository;
        public OrderConverter(
            IPhoneConverter phoneConverter,
            IPhoneRepository phoneRepository,
            IStatusRepository statusRepository,
            IAddressConverter addressConverter,
            IAddressRepository addressRepository,
            IOrderDetailRepository orderDetailRepository,
            IOrderStatusRepository orderStatusRepository
            )
        {
            _phoneConverter = phoneConverter;
            _phoneRepository = phoneRepository;
            _statusRepository = statusRepository;
            _addressConverter = addressConverter;
            _addressRepository = addressRepository;
            _orderDetailRepository = orderDetailRepository;
            _orderStatusRepository = orderStatusRepository;
        }
        //ORDER
        public async Task<OrderDto> ConvertToOrderDto(Order order)
        {
            var orderStatuses = await _orderStatusRepository.GetByOrderIdAsync(order.Id.ToString());
            var orderDetails = await _orderDetailRepository.GetByOrderIdAsync(order.Id.ToString());
            var address = await _addressRepository.GetOneAsync(order.AddressId.ToString());
            return new OrderDto()
            {
                Id = order.Id.ToString(),
                Address = address,
                CreateDate = order.CreateDate,
                PaymentStatus = order.PaymentStatus,
                PaymentMethod = order.PaymentMethod,
                OrderDetail = await ConvertToListOrderDetailDto(orderDetails),
                OrderStatus = await ConvertToListOrderStatusDto(orderStatuses),
            };
        }
        public async Task<IEnumerable<OrderDto>> ConvertToListOrderDto(IEnumerable<Order> orders)
        {
            List<OrderDto> result = new List<OrderDto>();
            foreach (var order in orders)
            {
                result.Add(await ConvertToOrderDto(order));
            }
            return result;
        }
        //ORDER STATUS
        public async Task<IEnumerable<OrderStatusDto>> ConvertToListOrderStatusDto(IEnumerable<OrderStatus> orderStatuses)
        {
            List<OrderStatusDto> orderStatusDtos = new List<OrderStatusDto>();
            foreach (var orderStatus in orderStatuses)
            {
                orderStatusDtos.Add(await ConvertToOrderStatusDto(orderStatus));
            }
            return orderStatusDtos;
        }
        public OrderStatus ConvertDtoToOrderStatus(OrderStatusDto dto)
        {
            return new OrderStatus()
            {
                Id = Guid.Parse(dto.Id),
                Created = (DateTime)dto.Created,
                OrderId = Guid.Parse(dto.OrderId),
                //StatusId = Guid.Parse(dto.StatusId)
            };
        }
        public async Task<OrderStatusDto> ConvertToOrderStatusDto(OrderStatus dto)
        {
            return new OrderStatusDto()
            {
                Id = dto.Id.ToString(),
                Created = dto.Created,
                OrderId = dto.OrderId.ToString(),
                Status = ConvertToStatusDto(await _statusRepository.GetAsync(dto.StatusId.ToString()))
            };
        }
        //ORDER DETAIL
        public async Task<OrderDetailDto> ConvertToOrderDetailDto(OrderDetail orderDetail)
        {
            return new OrderDetailDto()
            {
                Id = orderDetail.Id.ToString(),
                OrderId = orderDetail.OrderId.ToString(),
                PhoneId = orderDetail.PhoneId.ToString(),
                Phone = await _phoneConverter.ConvertToPhoneDto(
                    await _phoneRepository.GetOneAsync(orderDetail.PhoneId.ToString())),
                Quantity = orderDetail.Quantity
            };
        }
        public async Task<IEnumerable<OrderDetailDto>> ConvertToListOrderDetailDto(IEnumerable<OrderDetail> orderDetails)
        {
            List<OrderDetailDto> orderDetailDtos = new List<OrderDetailDto>();
            foreach (var orderDetail in orderDetails)
            {
                orderDetailDtos.Add(await ConvertToOrderDetailDto(orderDetail));
            }
            return orderDetailDtos;
        }
        //STATUS
        public StatusDto ConvertToStatusDto(Status status)
        {
            return new StatusDto()
            {
                Id = status.Id.ToString(),
                StatusType = status.StatusType,
            };
        }
    }
}

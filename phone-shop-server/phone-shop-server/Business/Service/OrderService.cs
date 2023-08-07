using phone_shop_server.Business.Converter;
using phone_shop_server.Business.DTO.Order;
using phone_shop_server.Database.Entity;
using phone_shop_server.Database.Enum;
using phone_shop_server.Database.Repository;

namespace phone_shop_server.Business.Service
{
    public interface IOrderService
    {
        Task<APIResponse.APIResponse> GetAllAsync();
        Task<APIResponse.APIResponse> GetOneAsync(string orderId);
        Task<APIResponse.APIResponse> GetByUserIdAsync(HttpContext context);
        Task<APIResponse.APIResponse> CancelOrder(HttpContext context, string orderId);
        Task<APIResponse.APIResponse> CreateAsync(OrderCreateDto orderCreateDto, HttpContext context);
        Task<APIResponse.APIResponse> UpdatePaymentStatus(OrderPaymentUpdateDto dto, HttpContext context);
        Task<APIResponse.APIResponse> UpdateOrderStatus(OrderStatusCreateDto dto, HttpContext context);

    }
    public class OrderService : IOrderService
    {
        private readonly IUserService _userService;
        private readonly IOrderConverter _orderConverter;
        private readonly IPhoneRepository _phoneRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IOrderStatusRepository _orderStatusRepository;
        public OrderService(
            IUserService userService,
            IOrderConverter orderConverter,
            IOrderRepository orderRepository,
            IPhoneRepository phoneRepository,
            IStatusRepository statusRepository,
            IOrderDetailRepository orderDetailRepository,
            IOrderStatusRepository orderStatusRepository
            )
        {
            _userService = userService;
            _orderConverter = orderConverter;
            _orderRepository = orderRepository;
            _phoneRepository = phoneRepository;
            _statusRepository = statusRepository;
            _orderDetailRepository = orderDetailRepository;
            _orderStatusRepository = orderStatusRepository;
        }

        public async Task<APIResponse.APIResponse> GetByUserIdAsync(HttpContext context)
        {
            try
            {
                string userId = await _userService.GetUserIdFromContext(context);
                if (userId == null)
                {
                    return new APIResponse.APIResponse()
                    {
                        code = StatusCode.ERROR.ToString(),
                        message = "user is null"
                    };
                }
                var orders = await _orderRepository.GetByUserIdAsync(userId);
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.SUCCESS.ToString(),
                    data = await _orderConverter.ConvertToListOrderDto(orders)
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
        public async Task<APIResponse.APIResponse> GetAllAsync()
        {
            try
            {
                var orders = await _orderRepository.GetAllAsync();
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.SUCCESS.ToString(),
                    data = await _orderConverter.ConvertToListOrderDto(orders)
                };
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.InnerException.ToString());
            }
        }
        public async Task<APIResponse.APIResponse> GetOneAsync(string orderId)
        {
            try
            {
                var order = await _orderRepository.GetAsync(orderId);
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.SUCCESS.ToString(),
                    data = await _orderConverter.ConvertToOrderDto(order)
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
        public async Task<APIResponse.APIResponse> CancelOrder(HttpContext context, string orderId)
        {
            try
            {
                var userId = await _userService.GetUserIdFromContext(context);
                var order = await _orderRepository.GetAsync(orderId);
                if (!order.UserId.Equals(userId))
                {
                    return new APIResponse.APIResponse()
                    {
                        code = StatusCode.ERROR.ToString(),
                        message = "user is not owner of order"
                    };
                }
                //update quantity in stock.
                var orderDetails = await _orderDetailRepository.GetByOrderIdAsync(orderId);
                foreach(var orderDetail in orderDetails)
                {
                    var phone = await _phoneRepository.GetOneAsync(orderDetail.PhoneId.ToString());
                    phone.Quantity += orderDetail.Quantity;
                    await _phoneRepository.UpdateAsync(phone);
                }
                var orderStatus = await _orderStatusRepository.CreateOrderStatusAsync(new OrderStatus()
                {
                    Created = DateTime.Now,
                    OrderId = order.Id,
                    StatusId = (await _statusRepository.GetByNameAsync(OrderStatusEnum.CANCELED.ToString())).Id
                });
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.ERROR.ToString(),
                    data = await _orderConverter.ConvertToOrderStatusDto(orderStatus)
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
        public async Task<APIResponse.APIResponse> CreateAsync(OrderCreateDto orderCreateDto, HttpContext context)
        {
            try
            {
                //check quuantity in stock
                foreach(var phone in orderCreateDto.phones)
                {
                    if((await _phoneRepository.GetOneAsync(phone.phoneId)).Quantity < phone.quantity)
                    {
                        return new APIResponse.APIResponse()
                        {
                            code = StatusCode.ERROR.ToString(),
                            message = "We are so sorry. Some phone is not enough in stock, please modify your order."
                        };
                    }
                }
                var createDate = DateTime.Now;
                var userId = await _userService.GetUserIdFromContext(context);
                if (userId == null)
                {
                    return new APIResponse.APIResponse()
                    {
                        code = StatusCode.ERROR.ToString(),
                        message = "user id is null"
                    };
                }
                var addressExist = await _userService.CheckAddressBelongToUser(userId.ToString(), orderCreateDto.addressId);
                if (addressExist == null)
                {
                    return new APIResponse.APIResponse()
                    {
                        code = StatusCode.ERROR.ToString(),
                        message = "address is null"
                    };
                }
                Order order = new Order()
                {
                    CreateDate = createDate,
                    PaymentMethod = orderCreateDto.paymentMethod,
                    PaymentStatus = orderCreateDto.paymentStatus,
                    AddressId = Guid.Parse(orderCreateDto.addressId),
                    UserId = await _userService.GetUserIdFromContext(context)
                };
                var orderResult = await _orderRepository.CreateAsync(order);
                var orderStatus = new OrderStatus()
                {
                    Created = createDate,
                    OrderId = orderResult.Id,
                    StatusId = (await _statusRepository.GetByNameAsync(OrderStatusEnum.CREATED.ToString())).Id,
                };
                await _orderStatusRepository.CreateOrderStatusAsync(orderStatus);
                foreach (var item in orderCreateDto.phones)
                {
                    await _orderDetailRepository.CreateAsync(new OrderDetail()
                    {
                        OrderId = orderResult.Id,
                        PhoneId = Guid.Parse(item.phoneId),
                        Quantity = item.quantity,
                    });
                }
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.SUCCESS.ToString(),
                    data = await _orderConverter.ConvertToOrderDto(orderResult)
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
        public async Task<APIResponse.APIResponse> UpdatePaymentStatus(OrderPaymentUpdateDto dto, HttpContext context)
        {
            try
            {
                string userId = await _userService.GetUserIdFromContext(context);
                var user = await _userService.getUserDetailAsync(userId);
                if(user == null)
                {
                    return new APIResponse.APIResponse()
                    {
                        code = StatusCode.ERROR.ToString(),
                        message = "user is null"
                    };
                }
                var order = await _orderRepository.GetAsync(dto.orderId);
                if(order == null)
                {
                    return new APIResponse.APIResponse()
                    {
                        code = StatusCode.ERROR.ToString(),
                        message = "order is null"
                    };
                }
                order.PaymentMethod = dto.paymentMethod;
                order.PaymentStatus = dto.paymentStatus;
                var orderUpdate = await _orderRepository.UpdateAsync(order);
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.SUCCESS.ToString(),
                    data = await _orderConverter.ConvertToOrderDto(orderUpdate)
                };
            }
            catch(Exception ex) 
            {
                throw new ApplicationException(ex.InnerException.ToString());
            }
        }

        public async Task<APIResponse.APIResponse> UpdateOrderStatus(OrderStatusCreateDto dto, HttpContext context)
        {
            try
            {
                var statusId = await _statusRepository.GetByNameAsync(dto.Status);
                await _orderStatusRepository.CreateOrderStatusAsync(new OrderStatus() {
                    OrderId = Guid.Parse(dto.OrderId),
                    StatusId = statusId.Id,
                    Created = DateTime.Now
                });
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.SUCCESS.ToString(),
                    message = "create success"
                };
            }
            catch (Exception ex)
            {
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.ERROR.ToString(),
                    message = "create failed"
                };
            }
        }
    }
}

using phone_shop_server.Business.DTO.Order;
using phone_shop_server.Database.Entity;

namespace phone_shop_server.Business.Converter
{
    public interface IOrderConverter
    {

    }
    public class OrderConverter
    {
        public OrderStatusDto ConvertOrderStatusToDto(OrderStatus orderStatus)
        {
            return new OrderStatusDto()
            {
                Id = orderStatus.Id.ToString(),
                Created = orderStatus.Created,
                OrderId = orderStatus.OrderId.ToString(),
                //Status = orderStatus.StatusId.ToString()
            };
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
        //
        public OrderStatus convertToOrderStatus(OrderStatusCreateDto dto)
        {
            return new OrderStatus()
            {

            };
        }
    }
}

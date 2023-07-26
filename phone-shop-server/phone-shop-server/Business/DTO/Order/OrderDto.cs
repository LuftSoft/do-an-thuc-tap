using phone_shop_server.Business.DTO.Address;
using phone_shop_server.Business.DTO.User;
using phone_shop_server.Database.Entity;

namespace phone_shop_server.Business.DTO.Order
{
    public class OrderDto
    {
        public string? Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public AddressDto? Address { get; set; }
        public UserDto? User { get; set; }
        public IEnumerable<OrderDetailDto> OrderDetail { get; set; }
        public IEnumerable<OrderStatusDto> OrderStatus { get; set; }
    }
}

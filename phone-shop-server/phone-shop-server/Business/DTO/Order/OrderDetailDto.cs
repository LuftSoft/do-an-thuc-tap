using phone_shop_server.Business.DTO.Phone;

namespace phone_shop_server.Business.DTO.Order
{
    public class OrderDetailDto
    {
        public string? Id { get; set; }
        public string? PhoneId { get; set; }
        public PhoneDto? Phone { get; set; }
        public string? OrderId { get; set; }
        public int Quantity { get; set; }
    }
}

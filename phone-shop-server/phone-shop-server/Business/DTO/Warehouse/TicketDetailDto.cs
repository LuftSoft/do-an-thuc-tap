using phone_shop_server.Business.DTO.Phone;
using phone_shop_server.Database.Entity;

namespace phone_shop_server.Business.DTO.Warehouse
{
    public class TicketDetailDto
    {
        public string? Id { get; set; }
        public string? WarehouseTicketId { get; set; }
        public string? PhoneId { get; set; }
        public PhoneDto PhoneDto { get; set; }
        //giả sử giá nhập vào là không đổi bằng 80% giá của chiếc điện thoại
        public int Quantity { get; set; }
    }
}

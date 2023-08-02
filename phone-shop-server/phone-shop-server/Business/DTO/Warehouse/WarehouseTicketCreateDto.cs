using phone_shop_server.Business.DTO.Order;

namespace phone_shop_server.Business.DTO.Warehouse
{
    public class WarehouseTicketCreateDto
    {
        public string SupplierId { get; set; }
        public string UserId { get; set; }
        public DateTime Created { get; set; }
        public IEnumerable<OrderCreatePhoneDto> phones { get; set; }
    }
}

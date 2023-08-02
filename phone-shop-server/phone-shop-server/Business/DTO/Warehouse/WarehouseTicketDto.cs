using phone_shop_server.Business.DTO.Order;
using phone_shop_server.Business.DTO.Supplier;
using phone_shop_server.Database.Entity;

namespace phone_shop_server.Business.DTO.Warehouse
{
    public class WarehouseTicketDto
    {
        public string Id { get; set; }
        public string SupplierId { get; set; }
        public SupplierDto? supplierDto { get; set; }
        public string UserId { get; set; }
        public DateTime Created { get; set; }
        public IEnumerable<TicketDetailDto>? ticketDetailDtos { get; set; }
    }
}

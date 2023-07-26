using phone_shop_server.Database.Entity;

namespace phone_shop_server.Business.DTO.Order
{
    public class OrderStatusDto
    {
        public string? Id { get; set; }
        public string? OrderId { get; set; }
        public StatusDto Status { get; set; }
        public DateTime? Created { get; set; }
    }
}

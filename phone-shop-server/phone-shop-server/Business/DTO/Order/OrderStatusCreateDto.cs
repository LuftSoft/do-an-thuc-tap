using Microsoft.Identity.Client;

namespace phone_shop_server.Business.DTO.Order
{
    public class OrderStatusCreateDto
    {
        public string? OrderId { get; set; }
        public string? Status { get; set; }
    }
}

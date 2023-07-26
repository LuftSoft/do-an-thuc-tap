using phone_shop_server.Business.DTO.Phone;
using phone_shop_server.Business.DTO.User;
using phone_shop_server.Database.Entity;

namespace phone_shop_server.Business.DTO.Cart
{
    public class CartDto
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public int Quantity { get; set; }
        public PhoneDto? Phone { get; set; }
    }
}

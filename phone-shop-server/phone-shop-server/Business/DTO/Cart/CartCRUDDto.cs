namespace phone_shop_server.Business.DTO.Cart
{
    public class CartCRUDDto
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? PhoneId { get; set; }
        public int Quantity { get; set; }
    }
}

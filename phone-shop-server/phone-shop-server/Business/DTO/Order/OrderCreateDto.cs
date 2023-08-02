namespace phone_shop_server.Business.DTO.Order
{
    public class OrderCreateDto
    {
        public string addressId { set; get; }
        public string paymentMethod { set; get; }
        public string paymentStatus { set; get; }
        public List<OrderCreatePhoneDto> phones { set; get; } 
    }
    public class OrderCreatePhoneDto
    {
        public string phoneId { get; set; }
        public int quantity { get; set; }
    }
}

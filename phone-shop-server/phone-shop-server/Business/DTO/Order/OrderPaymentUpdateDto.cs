namespace phone_shop_server.Business.DTO.Order
{
    public class OrderPaymentUpdateDto
    {
        public string orderId { get; set; }
        public string paymentStatus { set; get; }
        public string paymentMethod { set; get; }
    }
}

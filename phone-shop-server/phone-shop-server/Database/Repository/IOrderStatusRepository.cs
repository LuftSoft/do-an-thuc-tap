using phone_shop_server.Database.Entity;

namespace phone_shop_server.Database.Repository
{
    public interface IOrderStatusRepository
    {
        Task<OrderStatus> GetCurrentStatus(string orderId);
        Task<IEnumerable<OrderStatus>> GetByOrderIdAsync(string orderId);
        Task<OrderStatus> CreateOrderStatusAsync(OrderStatus orderStatus);

    }
}

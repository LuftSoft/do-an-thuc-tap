using phone_shop_server.Database.Entity;

namespace phone_shop_server.Database.Repository
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order> GetAsync(string orderId);
        Task<Order> CreateAsync(Order order);
        Task<Order> UpdateAsync(Order order);
        Task<bool> DeleteAsync(string orderId);

    }
}

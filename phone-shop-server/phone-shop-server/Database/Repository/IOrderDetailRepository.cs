using phone_shop_server.Database.Entity;

namespace phone_shop_server.Database.Repository
{
    public interface IOrderDetailRepository
    {
        Task<IEnumerable<OrderDetail>> GetAllAsync();
        Task<OrderDetail> GetAsync(string orderDetailId);
        Task<IEnumerable<OrderDetail>> GetByOrderIdAsync(string orderId);
        Task<OrderDetail> CreateAsync(OrderDetail orderDetail);
        Task<OrderDetail> UpdateAsync(OrderDetail orderDetail);
        Task<bool> DeleteAsync(string orderDetailId);

    }
}

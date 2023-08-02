using Microsoft.EntityFrameworkCore;
using phone_shop_server.Business.DTO.Order;
using phone_shop_server.Database.Entity;

namespace phone_shop_server.Database.Repository
{
    public class OrderStatusRepository : IOrderStatusRepository
    {
        private readonly AppDbContext _appDbContext;
        public OrderStatusRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<OrderStatus> GetCurrentStatus(string orderId)
        {
            try
            {
                return  await _appDbContext.OrderStatus
                    .Where(os => os.OrderId.ToString().Equals(orderId))
                    .OrderByDescending(os => os.Created).Take(1).FirstOrDefaultAsync();
            }
            catch(Exception ex)
            {
                throw new ApplicationException(ex.InnerException.ToString());
            }
        }
        public async Task<IEnumerable<OrderStatus>> GetByOrderIdAsync(string orderId)
        {
            try
            {
                return await _appDbContext.OrderStatus.Where(os => os.OrderId.ToString().Equals(orderId)).ToListAsync();
            }
            catch(Exception ex)
            {
                throw new ApplicationException(ex.InnerException.ToString());
            }
        }
        public async Task<OrderStatus> CreateOrderStatusAsync(OrderStatus orderStatus)
        {
            try
            {
                _appDbContext.OrderStatus.Add(orderStatus);
                await _appDbContext.SaveChangesAsync();
                return orderStatus;
            }
            catch(Exception ex)
            {
                throw new ApplicationException(ex.InnerException.ToString());
            }
        }
    }
}

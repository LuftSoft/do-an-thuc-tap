using MailKit.Search;
using Microsoft.EntityFrameworkCore;
using phone_shop_server.Database.Entity;

namespace phone_shop_server.Database.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _appDbContext;
        public OrderRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            try
            {
                return await _appDbContext.Order.Include(o => o.Address).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        public async Task<Order> GetAsync(string orderId)
        {
            try
            {
                return await _appDbContext.Order.FirstOrDefaultAsync(o => o.Id.ToString().Equals(orderId));
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        public async Task<IEnumerable<Order>> GetByUserIdAsync(string userId)
        {
            return await _appDbContext.Order.Where(o => o.UserId.Equals(userId))
                .Include(o => o.Address).ToListAsync();
        }
        public async Task<Order> CreateAsync(Order order)
        {
            try
            {
                _appDbContext.Order.Add(order);
                int result = await _appDbContext.SaveChangesAsync();
                return order;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        public async Task<Order> UpdateAsync(Order order)
        {
            try
            {
                _appDbContext.Order.Update(order);
                int result = await _appDbContext.SaveChangesAsync();
                return order;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        public async Task<bool> DeleteAsync(string orderId)
        {
            try
            {
                var order = await _appDbContext.Order.FirstOrDefaultAsync(o => o.Id.ToString().Equals(orderId));
                _appDbContext.Order.Remove(order);
                int result = await _appDbContext.SaveChangesAsync();
                return result > 0;
            }catch(Exception ex) 
            {
                throw new ApplicationException(ex.Message);
            }
        }
    }
}

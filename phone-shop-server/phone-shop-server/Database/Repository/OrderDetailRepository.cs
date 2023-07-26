using Microsoft.EntityFrameworkCore;
using phone_shop_server.Database.Entity;

namespace phone_shop_server.Database.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly AppDbContext _appDbContext;
        public OrderDetailRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IEnumerable<OrderDetail>> GetAllAsync()
        {
            try
            {
                return await _appDbContext.OrderDetail.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        public async Task<OrderDetail> GetAsync(string orderDetailId)
        {
            try
            {
                return await _appDbContext.OrderDetail.FirstOrDefaultAsync(o => o.Id.ToString().Equals(orderDetailId));
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        public async Task<IEnumerable<OrderDetail>> GetByOrderIdAsync(string orderId)
        {
            try
            {
                return await _appDbContext.OrderDetail.Where(od => od.OrderId.ToString().Equals(orderId))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        public async Task<OrderDetail> CreateAsync(OrderDetail orderDetail)
        {
            try
            {
                _appDbContext.OrderDetail.Add(orderDetail);
                int result = await _appDbContext.SaveChangesAsync();
                return orderDetail;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        public async Task<OrderDetail> UpdateAsync(OrderDetail orderDetail)
        {
            try
            {
                _appDbContext.OrderDetail.Update(orderDetail);
                int result = await _appDbContext.SaveChangesAsync();
                return orderDetail;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        public async Task<bool> DeleteAsync(string orderDetailId)
        {
            try
            {
                var order = await _appDbContext.OrderDetail.FirstOrDefaultAsync(o => o.Id.ToString().Equals(orderDetailId));
                _appDbContext.OrderDetail.Remove(order);
                int result = await _appDbContext.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
    }
}

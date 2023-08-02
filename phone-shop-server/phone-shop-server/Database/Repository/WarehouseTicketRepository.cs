using Microsoft.EntityFrameworkCore;
using phone_shop_server.Database.Entity;

namespace phone_shop_server.Database.Repository
{
    public class WarehouseTicketRepository : IWarehouseTicketRepository
    {
        private readonly AppDbContext _appDbContext;
        public WarehouseTicketRepository(
            AppDbContext appDbContext
            )
        {
            _appDbContext = appDbContext;
        }
        public async Task<IEnumerable<WarehouseTicket>> GetAllAsync()
        {
            return await _appDbContext.WarehouseTicket.ToListAsync();
        }
        public async Task<IEnumerable<WarehouseTicket>> GetByUserIdAsync(string userId)
        {
            return await _appDbContext.WarehouseTicket
                .Where(dt => dt.UserId.ToString().Equals(userId))
                .ToListAsync();
        }
        //public async Task<IEnumerable<WarehouseTicket>> GetByPhoneIdAsync(string phoneId)
        //{
        //    return await _appDbContext.WarehouseTicket
        //        .Where(dt => dt.WarehouseTicketId.ToString().Equals(warehouseTicketId))
        //        .ToListAsync();
        //}
        public async Task<WarehouseTicket> GetOneAsync(string id)
        {
            return await _appDbContext.WarehouseTicket
                .FirstOrDefaultAsync(dt => dt.Id.ToString().Equals(id));
        }
        public async Task<WarehouseTicket> CreateAsync(WarehouseTicket warehouseTicket)
        {
            _appDbContext.WarehouseTicket.Add(warehouseTicket);
            await _appDbContext.SaveChangesAsync();
            return warehouseTicket;
        }
        public async Task<WarehouseTicket> UpdateAsync(WarehouseTicket warehouseTicket)
        {
            _appDbContext.WarehouseTicket.Update(warehouseTicket);
            await _appDbContext.SaveChangesAsync();
            return warehouseTicket;
        }
        public async Task<bool> DeleteAsync(string id)
        {
            var warehouseTicket = await GetOneAsync(id);
            _appDbContext.WarehouseTicket.Remove(warehouseTicket);
            int result = await _appDbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}

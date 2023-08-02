using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using phone_shop_server.Database.Entity;

namespace phone_shop_server.Database.Repository
{
    public class TicketDetailRepository : ITicketDetailRepository
    {
        private readonly AppDbContext _appDbContext;
        public TicketDetailRepository(
            AppDbContext appDbContext
            )
        {
            _appDbContext = appDbContext;
        }
        public async Task<IEnumerable<TicketDetail>> GetAllAsync()
        {
            return await _appDbContext.DetailTicket.ToListAsync();
        }
        public async Task<IEnumerable<TicketDetail>> GetByWarehouseTicketIdAsync(string warehouseTicketId)
        {
            return await _appDbContext.DetailTicket
                .Where(dt => dt.WarehouseTicketId.ToString().Equals(warehouseTicketId))
                .ToListAsync();
        }
        public async Task<TicketDetail> GetOneAsync(string id)
        {
            return await _appDbContext.DetailTicket
                .FirstOrDefaultAsync(dt => dt.Id.ToString().Equals(id));
        }
        public async Task<TicketDetail> CreateAsync(TicketDetail ticketDetail)
        {
            _appDbContext.DetailTicket.Add(ticketDetail);
            await _appDbContext.SaveChangesAsync();
            return ticketDetail;
        }
        public async Task<TicketDetail> UpdateAsync(TicketDetail ticketDetail)
        {
            _appDbContext.DetailTicket.Update(ticketDetail);
            await _appDbContext.SaveChangesAsync();
            return ticketDetail;
        }
        public async Task<bool> DeleteAsync(string id)
        {
            var ticketDetail = await GetOneAsync(id);
            _appDbContext.DetailTicket.Remove(ticketDetail);
            int result = await _appDbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}

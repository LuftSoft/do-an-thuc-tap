using Microsoft.EntityFrameworkCore;
using phone_shop_server.Database.Entity;

namespace phone_shop_server.Database.Repository
{
    public class StatusRepository : IStatusRepository
    {
        private readonly AppDbContext _appDbContext;
        public StatusRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IEnumerable<Status>> GetAllAsync()
        {
            return await _appDbContext.Status.ToListAsync();
        }
        public async Task<Status> GetAsync(string id)
        {
            return await _appDbContext.Status.FirstOrDefaultAsync(s => s.Id.ToString().Equals(id));
        }
        public async Task<Status> GetByNameAsync(string statusName)
        {
            return await _appDbContext.Status.FirstOrDefaultAsync(s => s.StatusType.Equals(statusName));
        }
    }
}

using phone_shop_server.Database.Entity;

namespace phone_shop_server.Database.Repository
{
    public interface ITicketDetailRepository
    {
        Task<bool> DeleteAsync(string id);
        Task<TicketDetail> GetOneAsync(string id);
        Task<IEnumerable<TicketDetail>> GetAllAsync();
        Task<TicketDetail> CreateAsync(TicketDetail ticketDetail);
        Task<TicketDetail> UpdateAsync(TicketDetail ticketDetail);
        Task<IEnumerable<TicketDetail>> GetByWarehouseTicketIdAsync(string warehouseTicketId);

    }
}

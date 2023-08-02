using phone_shop_server.Database.Entity;

namespace phone_shop_server.Database.Repository
{
    public interface IWarehouseTicketRepository
    {
        Task<bool> DeleteAsync(string id);
        Task<WarehouseTicket> GetOneAsync(string id);
        Task<IEnumerable<WarehouseTicket>> GetAllAsync();
        Task<IEnumerable<WarehouseTicket>> GetByUserIdAsync(string userId);
        Task<WarehouseTicket> CreateAsync(WarehouseTicket warehouseTicket);
        Task<WarehouseTicket> UpdateAsync(WarehouseTicket warehouseTicket);

    }
}

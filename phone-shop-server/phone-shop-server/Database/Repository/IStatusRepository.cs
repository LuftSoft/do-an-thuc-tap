using phone_shop_server.Database.Entity;

namespace phone_shop_server.Database.Repository
{
    public interface IStatusRepository
    {
        Task<IEnumerable<Status>> GetAllAsync();
        Task<Status> GetAsync(string id);
        Task<Status> GetByNameAsync(string statusName);

    }
}

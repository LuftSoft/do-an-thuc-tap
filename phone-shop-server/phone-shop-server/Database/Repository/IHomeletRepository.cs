using phone_shop_server.Database.Entity;

namespace phone_shop_server.Database.Repository
{
    public interface IHomeletRepository
    {
        Task<Homelet?> GetOneAsync(string? id);
        Task<IEnumerable<Homelet>?> GetByNameAsync(string? key);
        Task<IEnumerable<Homelet>?> GetByDistrictIdAsync(string? id);
        Task<string> GetHomeletAddress(string homeletId);

    }
}

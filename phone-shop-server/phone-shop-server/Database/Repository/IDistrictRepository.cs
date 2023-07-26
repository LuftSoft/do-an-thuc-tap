using phone_shop_server.Database.Entity;

namespace phone_shop_server.Database.Repository
{
    public interface IDistrictRepository
    {
        Task<District?> GetOneAsync(string? id);
        Task<IEnumerable<District>?> GetByNameAsync(string? key);
        Task<IEnumerable<District>?> GetByProvineIdAsync(string? id);

    }
}

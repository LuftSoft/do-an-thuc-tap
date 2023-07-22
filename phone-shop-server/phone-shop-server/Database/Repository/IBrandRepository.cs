using phone_shop_server.Database.Entity;

namespace phone_shop_server.Database.Repository
{
    public interface IBrandRepository
    {
        Task<IEnumerable<Brand>> GetAllAsync();
        Task<Brand> GetOneAsync(string id);
        Task<Brand> CreateAsync(Brand brand);
        Task<Brand> UpdateAsync(Brand brand);
        Task<bool> DeleteAsync(string id);

    }
}

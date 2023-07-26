using phone_shop_server.Database.Entity;

namespace phone_shop_server.Database.Repository
{
    public interface IProvinceRepository
    {
        Task<IEnumerable<Province>> GetListProvine();
        Task<Province?> FindByIdAsync(string? id);
        Task<IEnumerable<Province>?> FindByNameAsync(string? key);

    }
}

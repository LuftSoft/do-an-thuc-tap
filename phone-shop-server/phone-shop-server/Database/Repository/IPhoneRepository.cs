using phone_shop_server.Database.Entity;

namespace phone_shop_server.Database.Repository
{
    public interface IPhoneRepository
    {
        Task<IEnumerable<Phone>> GetAllAsync();
        Task<Phone> GetOneAsync(string id);
        Task<Phone> CreateAsync(Phone phone);
        Task<Phone> UpdateAsync(Phone phone);
        Task<bool> DeleteAsync(string id);


    }
}

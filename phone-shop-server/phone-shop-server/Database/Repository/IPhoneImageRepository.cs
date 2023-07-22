using phone_shop_server.Database.Entity;

namespace phone_shop_server.Database.Repository
{
    public interface IPhoneImageRepository
    {
        Task<PhoneImage> GetOneAsync(string id);
        Task<IEnumerable<PhoneImage>> GetByPhoneIdAsync(string id);
        Task<PhoneImage> CreateAsync(PhoneImage phoneImage);
        Task<bool> DeleteAsync(string id);

    }
}

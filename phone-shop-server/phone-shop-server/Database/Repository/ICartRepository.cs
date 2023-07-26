using phone_shop_server.Database.Entity;

namespace phone_shop_server.Database.Repository
{
    public interface ICartRepository
    {
        Task<IEnumerable<Cart>> GetAllAsync(string userId);
        Task<Cart> GetAsync(int cartId);
        Task<Cart> CreateAsync(Cart cart);
        Task<Cart> UpdateAsync(Cart cart);
        Task<bool> DeleteAsync(int cartId);

    }
}

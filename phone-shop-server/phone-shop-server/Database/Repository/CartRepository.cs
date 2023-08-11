using Microsoft.EntityFrameworkCore;
using phone_shop_server.Business.DTO.Cart;
using phone_shop_server.Database.Entity;

namespace phone_shop_server.Database.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _appDbContext;
        public CartRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IEnumerable<Cart>> GetAllAsync(string userId)
        {
            return await _appDbContext.Cart.Where(c => c.UserId.Equals(userId)).ToListAsync();
        }
        public async Task<Cart> GetAsync(int cartId)
        {
            return await _appDbContext.Cart.FirstOrDefaultAsync(c => c.Id == cartId);
        }
        public async Task<Cart> GetByPhoneAndUserAsync(string phoneId, string userId)
        {
            return await _appDbContext.Cart
                .FirstOrDefaultAsync(c => c.PhoneId.ToString() == phoneId && c.UserId.Equals(userId));
        }
        public async Task<Cart> CreateAsync(Cart cart)
        {
            _appDbContext.Cart.Add(cart);
            await _appDbContext.SaveChangesAsync();
            return cart;
        }
        public async Task<Cart> UpdateAsync(Cart cart)
        {
            if(cart.Quantity == 0)
            {
                await DeleteAsync(cart.Id);
                return cart;
            }
            var cartUpdate = await GetAsync(cart.Id);
            cartUpdate.Quantity = cart.Quantity;
            _appDbContext.Cart.Update(cartUpdate);
            await _appDbContext.SaveChangesAsync();
            return cart;
        }
        public async Task<bool> DeleteAsync(int cartId)
        {
            _appDbContext.Cart.Remove(_appDbContext.Cart.FirstOrDefault(c => c.Id == cartId));  
            int result = await _appDbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}

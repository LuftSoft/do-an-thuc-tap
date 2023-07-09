using phone_shop_server.Database.Entity;

namespace phone_shop_server.Database.Repository
{
    public class UserRepository
    {
        private readonly AppDbContext _appDbContext;
        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public AppUser Get(string id)
        {
            return _appDbContext.Users.SingleOrDefault(u => u.Id == id);
        }
    }
}

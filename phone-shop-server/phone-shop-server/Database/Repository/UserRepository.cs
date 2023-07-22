using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MimeKit.Encodings;
using phone_shop_server.Database.Entity;

namespace phone_shop_server.Database.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<AppUser> _userManager;
        public UserRepository(
            AppDbContext appDbContext,
            UserManager<AppUser> userManager
            )
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }
        public async Task<IEnumerable<AppUser>> GetAllAsync()
        {
            return await _appDbContext.Users.ToListAsync();
        }
        public async Task<IEnumerable<AppUser>> GetAllStaffAsync()
        {
            string id = (await _appDbContext.Roles.FirstOrDefaultAsync(r => r.NormalizedName.Equals("STAFF"))).Id;
            return await _appDbContext.Users.ToListAsync();
        }
        public async Task<AppUser> GetAsync(string id)
        {
            return await _appDbContext.Users.SingleOrDefaultAsync(u => u.Id == id);
        }
        public async Task<AppUser> CreateAsync(AppUser user)
        {
            _appDbContext.Users.Add(user);
            await _appDbContext.SaveChangesAsync();
            return user;
        }
        public async Task<AppUser> UpdateAsync(AppUser user)
        {
            _appDbContext.Users.Update(user);
            await _appDbContext.SaveChangesAsync();
            return user;
        }
        public async Task<bool> DeleteAsync(string userId)
        {
            _appDbContext.Users.Remove(await this.GetAsync(userId));
            int result = await _appDbContext.SaveChangesAsync();
            return result > 0;
        }
        public async Task<bool> CheckPassWord(string userId, string password)
        {
            return await _userManager.CheckPasswordAsync(await this.GetAsync(userId), password);
        }
        public async Task<bool> ChangePassword(string userId, string currentPassword, string newPassword)
        {
            IdentityResult result = await _userManager.ChangePasswordAsync(await this.GetAsync(userId), currentPassword, newPassword);
            return result.Succeeded;
        }
        public async Task<bool> ResetPassword(string userId, string newPassword)
        {
            try
            {
                AppUser user = await this.GetAsync(userId);
                string newPasswordHash = _userManager.PasswordHasher.HashPassword(user, newPassword);
                user.PasswordHash = newPasswordHash;
                await this.UpdateAsync(user);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> addUserRoleAsync(AppUser user, string role)
        {
            return (await _userManager.AddToRoleAsync(user, role)).Succeeded;
        }
        public async Task<bool> removeUserRoleAsync(AppUser user, string role)
        {
            return (await _userManager.RemoveFromRoleAsync(user, role)).Succeeded;
        }
    }
}

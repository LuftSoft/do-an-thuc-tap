using phone_shop_server.Database.Entity;

namespace phone_shop_server.Database.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> GetAllAsync();
        Task<IEnumerable<AppUser>> GetAllStaffAsync();
        Task<AppUser> GetAsync(string id);
        Task<AppUser> CreateAsync(AppUser user);
        Task<AppUser> UpdateAsync(AppUser user);
        Task<bool> DeleteAsync(string userId);
        Task<bool> CheckPassWord(string userId, string password);
        Task<bool> ChangePassword(string userId, string currentPassword, string newPassword);
        Task<bool> ResetPassword(string userId, string newPassword);
        Task<bool> addUserRoleAsync(AppUser user, string role);
        Task<bool> removeUserRoleAsync(AppUser user, string role);
        Task<IEnumerable<string>> GetListRoleAsync(string userId);
        Task<bool> IsEmailExist(string email);
        Task<bool> IsPhoneNumberExist(string phonenumber);
        Task<AppUser> GetByEmailAsync(string email);
        Task<bool> AddNewPassword(AppUser user, string newPassword);
    }
}

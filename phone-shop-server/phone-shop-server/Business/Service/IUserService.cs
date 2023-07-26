using phone_shop_server.Business.DTO.User;

namespace phone_shop_server.Business.Service
{
    public interface IUserService
    {
        Task<string?> GetUserIdFromToken(string token);
        Task<string?> GetUserIdFromContext(HttpContext context);
        Task<string> GetTokenFromContext(HttpContext context);
        Task<UserDto?> getUserDetailAsync(string userId);
        Task<bool> DeleteAsync(string delUserId);
        Task<APIResponse.APIResponse> UpdateAsync(UserUpdateDto dto);
        Task<APIResponse.APIResponse> LoginService(UserLoginDto dto);
        Task<APIResponse.APIResponse> GetAllUser();
        Task<APIResponse.APIResponse> AddRoleToUser(string userId, string role);
        Task<APIResponse.APIResponse> RemoveRoleFromUser(string userId, string role);
        Task<APIResponse.APIResponse> SignupService(UserSignUpDto dto);
        Task<APIResponse.APIResponse> fogotPasswordService(FogotPasswordDto dto);
        Task<APIResponse.APIResponse> resetPasswordService(ResetPasswordDto dto);
        Task<APIResponse.APIResponse> changePasswordService(ChangePasswordDto dto, HttpContext context);
        Task<APIResponse.APIResponse> RefreshToken(string token, HttpContext context);
    }
}

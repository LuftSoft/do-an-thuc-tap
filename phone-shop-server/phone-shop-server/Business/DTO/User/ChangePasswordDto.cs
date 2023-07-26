namespace phone_shop_server.Business.DTO.User
{
    public class ChangePasswordDto
    {
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
    }
}

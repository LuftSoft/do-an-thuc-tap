namespace phone_shop_server.Business.DTO.User
{
    public class UserSignUpDto
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public IFormFile? Avatar { get; set; }
        public string? Role { get; set; }
    }
}

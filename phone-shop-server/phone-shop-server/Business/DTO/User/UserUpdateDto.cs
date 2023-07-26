using phone_shop_server.Business.DTO.Address;

namespace phone_shop_server.Business.DTO.User
{
    public class UserUpdateDto
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Age { get; set; }
        public string? PhoneNumber { get; set; }
        public IFormFile? Avatar { get; set; }
    }
}

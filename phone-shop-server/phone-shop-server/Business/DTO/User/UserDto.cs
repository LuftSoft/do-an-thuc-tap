using Microsoft.EntityFrameworkCore.Metadata.Internal;
using phone_shop_server.Business.DTO.Address;
using phone_shop_server.Database.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace phone_shop_server.Business.DTO.User
{
    public class UserDto
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public int? Age { get; set; }
        public string? Avatar { get; set; }
        public string? ResetPasswordToken { get; set; }
        public string? RefreshToken { get; set; }
        public bool IsBlock { get; set; }
        public List<AddressDto>? Address { get; set; }
        public List<string>? Role { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace phone_shop_server.Database.Entity
{
    public class AppUser : IdentityUser
    {
        [Column(TypeName = "nvarchar(100)")]
        public string? FirstName { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? LastName { get; set; }
        public int? Age { get; set; }
        public string? Avatar { get; set; }
        [Column(TypeName = "nvarchar(1000)")]
        public string? ResetPasswordToken { get; set; }
        [Column(TypeName = "nvarchar(1000)")]
        public string? RefreshToken { get; set; }
        public bool IsBlock { get; set; }
    }
}

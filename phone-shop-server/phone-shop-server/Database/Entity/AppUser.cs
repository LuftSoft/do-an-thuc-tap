using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace phone_shop_server.Database.Entity
{
    [Table("User")]
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
        public ICollection<Address> Addresses { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public ICollection<WarehouseTicket> WarehouseTicket { get; set;}
    }
}

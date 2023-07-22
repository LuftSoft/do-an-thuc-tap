using System.ComponentModel.DataAnnotations.Schema;

namespace phone_shop_server.Database.Entity
{
    [Table("Cart")]
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public Guid PhoneId { get; set; }
        public Phone Phone { get; set; }
        public int Quantity { get; set; }
    }
}

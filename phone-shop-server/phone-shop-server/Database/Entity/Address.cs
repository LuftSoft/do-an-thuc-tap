using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace phone_shop_server.Database.Entity
{
    [Table("Address")]
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string DetailAddress { get; set; }
        //nha rieng, van phong, ...
        public string Type { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public string HomeletId { get; set; }
        public Homelet Homelet { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}

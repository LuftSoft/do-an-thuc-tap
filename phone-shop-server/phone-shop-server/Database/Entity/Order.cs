using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace phone_shop_server.Database.Entity
{
    [Table("Order")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid AddressId { get; set; }
        public Address Address { get; set; }
        public string UserId { get; set; }
        public string PaymentStatus { set; get; }
        public string PaymentMethod { set; get; }
        public string? PaymentOnlineReceipt { set; get; }
        public ICollection<OrderDetail> OrderDetail { get; set; }
        public ICollection<OrderStatus> OrderStatus { get; set; }

    }
}

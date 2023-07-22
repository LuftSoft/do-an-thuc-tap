using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace phone_shop_server.Database.Entity
{
    [Table("OrderStatus")]
    public class OrderStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Guid StatusId { get; set; }
        public Status Status { get; set; }
        public DateTime Created { get; set; }
    }
}

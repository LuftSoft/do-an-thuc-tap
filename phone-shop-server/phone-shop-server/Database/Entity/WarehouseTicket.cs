using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace phone_shop_server.Database.Entity
{
    [Table("WarehouseTicket")]
    public class WarehouseTicket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public DateTime Created { get; set; }
        public ICollection<TicketDetail> TicketDetails { get; set; }
    }
}

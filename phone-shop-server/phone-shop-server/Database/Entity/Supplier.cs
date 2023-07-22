using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace phone_shop_server.Database.Entity
{
    [Table("Supplier")]
    public class Supplier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public ICollection<WarehouseTicket> WarehouseTickets { get; set; }
        public string HomeletId { get; set; }
        public Homelet Homelet { get; set; }
    }
}

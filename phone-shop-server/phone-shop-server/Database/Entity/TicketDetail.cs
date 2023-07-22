using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace phone_shop_server.Database.Entity
{
    [Table("TicketDetail")]
    public class TicketDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid WarehouseTicketId { get; set; }
        public WarehouseTicket WarehouseTicket { get; set; }
        public Guid PhoneId { get; set; }
        public Phone Phone { get; set; }
        //giả sử giá nhập vào là không đổi bằng 80% giá của chiếc điện thoại
        public int Quantity { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace phone_shop_server.Database.Entity
{
    [Table("PromoteDetail")]
    public class PromoteDetail
    {
        public Guid Id { get; set; }
        public Guid PhoneId { get; set; }
        public Phone Phone { get; set; }
        public Guid PromoteId { get; set; }
        public Promote Promote { get; set; }
        public double Percent { get; set; }
    }
}

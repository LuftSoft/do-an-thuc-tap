using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace phone_shop_server.Database.Entity
{
    [Table("PhoneImage")]
    public class PhoneImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Link { get; set; }
        public Guid PhoneId { get; set; }
        public Phone Phone { get; set; }
    }
}

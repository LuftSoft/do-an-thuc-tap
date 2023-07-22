using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace phone_shop_server.Database.Entity
{
    [Table("Homelet")]
    public class Homelet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string DistrictId { get; set; }
        public District District { get; set; }
        public ICollection<Address> Address { get; set; }
        public ICollection<Supplier> Supplier { get; set; }
    }
}

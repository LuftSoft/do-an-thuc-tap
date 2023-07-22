using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace phone_shop_server.Database.Entity
{
    [Table("District")]
    public class District
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string ProvinceId { get; set; }
        public Province Province { get; set; }
        public ICollection<Homelet> Homelet { get; set; }   
    }
}

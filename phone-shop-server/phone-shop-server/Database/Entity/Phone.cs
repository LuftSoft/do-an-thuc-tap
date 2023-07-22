using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace phone_shop_server.Database.Entity
{
    [Table("Phone")]
    public class Phone
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Operation { get; set; }
        public string CPU { get; set; }
        public string RAM { get; set; }
        public string ROM { get; set; }
        public string PIN { get; set; }
        public double ScreenSize { get; set; }
        public double ScreenResolution { get; set; }
        public string FrontCamera { get; set; }
        public string BehindCamera { get; set; }
        //mat kinh cam ung
        public string ScreenTouch { get; set; }
        public string OtherBenefit { get; set; }
        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }
        //stock
        public int Quantity { get; set; }
        public double ImportPrice { get; set; }
        public double SoldPrice { get; set; }
        public ICollection<PhoneImage> PhoneImages { get; set; }
        public ICollection<PromoteDetail> PromoteDetails { get; set; }
        public ICollection<TicketDetail> TicketDetails { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

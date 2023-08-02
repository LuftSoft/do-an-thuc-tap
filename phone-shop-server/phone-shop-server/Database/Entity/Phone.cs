using Microsoft.EntityFrameworkCore;
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
        [Column(TypeName = "nvarchar(1000)")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string Operation { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string CPU { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string RAM { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string ROM { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string PIN { get; set; }
        [Range(0, 100)]
        public double ScreenSize { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string ScreenResolution { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string FrontCamera { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string BehindCamera { get; set; }
        [Column(TypeName = "nvarchar(1000)")]
        public string Slug { get; set; }
        //mat kinh cam ung
        public string ScreenTouch { get; set; }
        public string OtherBenefit { get; set; }
        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }
        //stock
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
        [Range(0, Double.MaxValue)]
        public double ImportPrice { get; set; }
        [Range(0, Double.MaxValue)]
        public double SoldPrice { get; set; }
        public ICollection<PhoneImage> PhoneImages { get; set; }
        public ICollection<PromoteDetail> PromoteDetails { get; set; }
        public ICollection<TicketDetail> TicketDetails { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

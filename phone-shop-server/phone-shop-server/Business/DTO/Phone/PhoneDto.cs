using phone_shop_server.Business.DTO.Brand;
using phone_shop_server.Business.DTO.PhoneImage;
using phone_shop_server.Database.Entity;

namespace phone_shop_server.Business.DTO.Phone
{
    public class PhoneDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Operation { get; set; }
        public string? CPU { get; set; }
        public string? RAM { get; set; }
        public string? ROM { get; set; }
        public string? PIN { get; set; }
        public double? ScreenSize { get; set; }
        public string? ScreenResolution { get; set; }
        public string? FrontCamera { get; set; }
        public string? BehindCamera { get; set; }
        //mat kinh cam ung
        public string? ScreenTouch { get; set; }
        public string? OtherBenefit { get; set; }
        public int? Quantity { get; set; }
        public double ImportPrice { get; set; }
        public double SoldPrice { get; set; }
        public string Slug { get; set; }
        public BrandDto Brand { get; set; }
        public List<PhoneImageDto>? PhoneImages { get; set; }
    }
}

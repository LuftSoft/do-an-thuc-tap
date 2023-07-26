using phone_shop_server.Database.Entity;

namespace phone_shop_server.Business.DTO.Address
{
    public class AddressDto
    {
        public string? Id { get; set; }
        public string? DetailAddress { get; set; }
        //nha rieng, van phong, ...
        public string? Type { get; set; }
        public string? UserId { get; set; }
        public string? HomeletId { get; set; }
        public string? HomeletAddress { get; set; }
    }
}

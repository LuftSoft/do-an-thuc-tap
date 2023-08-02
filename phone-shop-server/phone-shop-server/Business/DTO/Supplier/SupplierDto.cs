using phone_shop_server.Database.Entity;

namespace phone_shop_server.Business.DTO.Supplier
{
    public class SupplierDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string HomeletId { get; set; }
        public string DetailLocation { get; set; }
    }
}

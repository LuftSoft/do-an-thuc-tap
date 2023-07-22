namespace phone_shop_server.Business.DTO.Brand
{
    public class BrandCreateDto
    {
        public string Name { get; set; }
        public IFormFile Logo { get; set; }
        public string Description { get; set; }
    }
}

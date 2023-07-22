using phone_shop_server.Database.Enum;

namespace phone_shop_server.Business.APIResponse
{
    public class APIResponse
    {
        public string? code { set; get; }
        public string? message { set; get; }
        public object? data { set; get; }
    }
}

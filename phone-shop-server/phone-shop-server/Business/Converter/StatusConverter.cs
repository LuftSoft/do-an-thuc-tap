using phone_shop_server.Business.DTO.Order;
using phone_shop_server.Database.Entity;

namespace phone_shop_server.Business.Converter
{
    public interface IStatusConverter
    {

    }
    public class StatusConverter : IStatusConverter
    {
        public StatusDto ConvertToStatusDto(Status status)
        {
            return new StatusDto()
            {
                Id = status.Id.ToString(),
                StatusType = status.StatusType,
            };
        }
    }
}

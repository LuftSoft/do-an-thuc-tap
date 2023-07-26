using phone_shop_server.Business.DTO.Address;
using phone_shop_server.Database.Entity;
using phone_shop_server.Database.Repository;

namespace phone_shop_server.Business.Converter
{
    public interface IAddressConverter
    {
        Task<AddressDto> ConvertAddressToAddressDto(Address address);
        Task<IEnumerable<AddressDto>> ConvertToListAddressDto(IEnumerable<Address> address);

    }
    public class AddressConverter : IAddressConverter
    {
        private readonly IHomeletRepository _homeletRepository;
        public AddressConverter(
            IHomeletRepository homeletRepository
            )
        {
            _homeletRepository = homeletRepository;
        }
        public async Task<AddressDto> ConvertAddressToAddressDto(Address address)
        {
            return new AddressDto()
            {
                Id = address.Id.ToString(),
                Type = address.Type,
                UserId = address.UserId,
                HomeletId = address.HomeletId,
                DetailAddress = address.DetailAddress,
                HomeletAddress = await _homeletRepository.HomeletAddress(address.HomeletId)
            };
        }
        public async Task<IEnumerable<AddressDto>> ConvertToListAddressDto(IEnumerable<Address> address)
        {
            List<AddressDto> addressDtos = new List<AddressDto>();
            foreach (var ad in address)
            {
                addressDtos.Add(await ConvertAddressToAddressDto(ad));
            }
            return addressDtos;
        }
    }
}

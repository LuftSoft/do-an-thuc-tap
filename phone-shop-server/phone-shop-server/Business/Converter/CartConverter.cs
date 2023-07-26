using phone_shop_server.Business.DTO.Cart;
using phone_shop_server.Business.Service;
using phone_shop_server.Database.Entity;

namespace phone_shop_server.Business.Converter
{
    public interface ICartConverter
    {
        Task<CartDto> ConvertCartToCartDto(Cart cart);
        Task<IEnumerable<CartDto>> ConvertCartToListCartDto(IEnumerable<Cart> carts);
        Cart ConvertCartDtoToCart(CartDto cartDto);
        IEnumerable<Cart> ConvertCartToListCartDto(IEnumerable<CartDto> cartDtos);
        Cart ConvertCartCRUDDtoToCart(CartCRUDDto dto);

    }
    public class CartConverter : ICartConverter
    {
        private readonly IPhoneService _phoneService;
        public CartConverter(
            IPhoneService phoneService
            ) 
        {
            _phoneService = phoneService;
        }
        public async Task<CartDto> ConvertCartToCartDto(Cart cart)
        {
            return new CartDto()
            {
                Id = cart.Id,
                Phone = await _phoneService.GetOneAsync(cart.PhoneId.ToString()),
                UserId = cart.UserId,
                Quantity = cart.Quantity,
            };
        }
        public async Task<IEnumerable<CartDto>> ConvertCartToListCartDto(IEnumerable<Cart> carts)
        {
            List<CartDto> cartList = new List<CartDto>();
            foreach (Cart cart in carts)
            {
                cartList.Add(await ConvertCartToCartDto(cart));
            }
            return cartList;
        }
        public Cart ConvertCartDtoToCart(CartDto cartDto)
        {
            return new Cart()
            {
                Id = cartDto.Id,
                PhoneId = Guid.Parse(cartDto.Phone.Id),
                UserId = cartDto.UserId,
                Quantity = cartDto.Quantity,
            };
        }
        public IEnumerable<Cart> ConvertCartToListCartDto(IEnumerable<CartDto> cartDtos)
        {
            List<Cart> cartList = new List<Cart>();
            foreach (CartDto cart in cartDtos)
            {
                cartList.Add(ConvertCartDtoToCart(cart));
            }
            return cartList;
        }
        public Cart ConvertCartCRUDDtoToCart(CartCRUDDto dto)
        {
            return new Cart()
            {
                Id = dto.Id,
                PhoneId = Guid.Parse(dto.PhoneId),
                UserId = dto.UserId,
                Quantity = dto.Quantity
            };
        }
    }
}

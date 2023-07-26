using phone_shop_server.Business.APIResponse;
using phone_shop_server.Business.Converter;
using phone_shop_server.Business.DTO.Cart;
using phone_shop_server.Database.Entity;
using phone_shop_server.Database.Enum;
using phone_shop_server.Database.Repository;

namespace phone_shop_server.Business.Service
{
    public interface ICartService
    {
        Task<APIResponse.APIResponse> RemoveFromCartAsync(int cartId, HttpContext context);
        Task<APIResponse.APIResponse> GetAllAsync(HttpContext context);
        Task<APIResponse.APIResponse> AddToCartAsync(HttpContext context, CartCRUDDto dto);

    }
    public class CartService : ICartService
    {
        private readonly IUserService _userService;
        private readonly ICartRepository _cartRepository;
        private readonly ICartConverter _cartConverter;
        public CartService(
            IUserService userService,
            ICartRepository cartRepository,
            ICartConverter cartConverter
            )
        {
            _userService = userService;
            _cartConverter = cartConverter;
            _cartRepository = cartRepository;
        }
        public async Task<APIResponse.APIResponse> GetAllAsync(HttpContext context)
        {
            try
            {
                string? userId = await _userService.GetUserIdFromContext(context);
                IEnumerable<Cart> carts = await _cartRepository.GetAllAsync(userId);
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.SUCCESS.ToString(),
                    data = await _cartConverter.ConvertCartToListCartDto(carts)
                };
            }
            catch (Exception ex)
            {
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.ERROR.ToString(),
                    message = ex.Message
                };
            }
        }
        public async Task<APIResponse.APIResponse> AddToCartAsync(HttpContext context, CartCRUDDto dto)
        {
            try
            {
                var cartExist = await _cartRepository.GetAsync(dto.Id);
                string userId = await _userService.GetUserIdFromContext(context);
                dto.UserId = userId;
                if (cartExist == null)
                {
                    //add to cart
                    var cartCreate = await _cartRepository.CreateAsync(_cartConverter.ConvertCartCRUDDtoToCart(dto));
                    return new APIResponse.APIResponse()
                    {
                        code = StatusCode.SUCCESS.ToString(),
                        data = await _cartConverter.ConvertCartToCartDto(cartCreate)
                    };
                }
                //edit cart
                var cartUpdate = await _cartRepository.UpdateAsync(_cartConverter.ConvertCartCRUDDtoToCart(dto));
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.SUCCESS.ToString(),
                    data = await _cartConverter.ConvertCartToCartDto(cartUpdate)
                };
            }
            catch (Exception ex)
            {
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.ERROR.ToString(),
                    message = ex.Message
                };
            }
        }
        public async Task<APIResponse.APIResponse> RemoveFromCartAsync(int cartId, HttpContext context)
        {
            try
            {
                Cart cart = await _cartRepository.GetAsync(cartId);
                if (!cart.UserId.Equals(await _userService.GetUserIdFromContext(context)))
                {
                    return new APIResponse.APIResponse()
                    {
                        code = StatusCode.ERROR.ToString(),
                        message = "user is not owner of cart"
                    };
                }
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.SUCCESS.ToString(),
                    data = await _cartRepository.DeleteAsync(cartId)
                };
            }
            catch (Exception ex)
            {
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.ERROR.ToString(),
                    message = ex.Message
                };
            }
        }
    }
}

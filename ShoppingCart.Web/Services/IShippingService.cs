using ShoppingCart.Web.Models;

namespace ShoppingCart.Web.Services;

public interface IShippingService
{
    void Ship(IAddressInfo info, IEnumerable<ICartItem> items);
}
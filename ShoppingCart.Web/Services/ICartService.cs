using ShoppingCart.Web.Models;

namespace ShoppingCart.Web.Services;

public interface ICartService
{
    IEnumerable<ICartItem> Items();
    decimal Total();
}
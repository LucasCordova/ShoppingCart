using ShoppingCart.Web.Models;

namespace ShoppingCart.Web.Services;

public interface IPaymentService
{
    public bool Charge(decimal amount, ICard creditCard);
}
namespace ShoppingCart.Web.Models;

public interface ICartItem
{
    public string ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
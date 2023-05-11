namespace ShoppingCart.Web.Models;

public interface ICard
{
    public string CardNumber { get; set; }
    public string Name { get; set; }
    public DateTime ValidTo { get; set; }
}
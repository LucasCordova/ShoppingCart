using Moq;
using ShoppingCart.Web.Controllers;
using ShoppingCart.Web.Models;
using ShoppingCart.Web.Services;

namespace ShoppingCartTests;

public class CartControllerTests
{
    private readonly Mock<IAddressInfo> _addressInfoMock = new();
    private readonly Mock<ICard> _cardMock = new();
    private readonly Mock<ICartService> _cartServiceMock = new();
    private readonly Mock<IPaymentService> _paymentServiceMock = new();
    private readonly Mock<IShippingService> _shipmentServiceMock = new();
    private CartController? _controller;
    private List<ICartItem>? _items;

    [SetUp]
    public void Setup()
    {
        // arrange
        var cartItemMock = new Mock<ICartItem>();
        cartItemMock.Setup(item => item.Price).Returns(10M);

        _items = new List<ICartItem>
        {
            cartItemMock.Object
        };

        _cartServiceMock.Setup(cartService => cartService.Items()).Returns(_items.AsEnumerable());

        _controller = new CartController(null, _cartServiceMock.Object, _paymentServiceMock.Object,
            _shipmentServiceMock.Object);
    }

    [Test]
    public void ShouldReturnCharged()
    {
        _paymentServiceMock.Setup(paymentService => paymentService.Charge(It.IsAny<decimal>(), _cardMock.Object))
            .Returns(true);

        // act
        var result = _controller?.CheckOut(_cardMock.Object, _addressInfoMock.Object);

        // assert
        _shipmentServiceMock.Verify(
            shippingService => shippingService.Ship(_addressInfoMock.Object, _items!.AsEnumerable()), Times.Once());

        Assert.That(result, Is.EqualTo("charged"));
    }

    [Test]
    public void ShouldReturnNotCharged()
    {
        _paymentServiceMock.Setup(paymentService => paymentService.Charge(It.IsAny<decimal>(), _cardMock.Object))
            .Returns(false);

        // act
        var result = _controller?.CheckOut(_cardMock.Object, _addressInfoMock.Object);

        // assert
        _shipmentServiceMock.Verify(
            shippingService => shippingService.Ship(_addressInfoMock.Object, _items!.AsEnumerable()), Times.Never());
        Assert.That(result, Is.EqualTo("not charged"));
    }
}
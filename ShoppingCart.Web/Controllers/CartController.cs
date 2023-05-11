using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Web.Models;
using ShoppingCart.Web.Services;

namespace ShoppingCart.Web.Controllers;

public class CartController : Controller
{
    private readonly ICartService _cartService;
    private readonly ILogger<CartController>? _logger;
    private readonly IPaymentService _paymentService;
    private readonly IShippingService _shippingService;

    public CartController(ILogger<CartController>? logger,
        ICartService cartService,
        IPaymentService paymentService,
        IShippingService shippingService)
    {
        _logger = logger;
        _cartService = cartService;
        _paymentService = paymentService;
        _shippingService = shippingService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    public string CheckOut(ICard card, IAddressInfo addressInfo)
    {
        var result = _paymentService.Charge(_cartService.Total(), card);
        if (!result) return "not charged";

        _shippingService.Ship(addressInfo, _cartService.Items());
        return "charged";
    }
}
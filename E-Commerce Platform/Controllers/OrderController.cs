using E_Commerce_Platform.Contracts;
using E_Commerce_Platform.DataBase;
using E_Commerce_Platform.DataBase.Models;
using E_Commerce_Platform.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Platform.Controllers;

[Route("order")]
[Authorize]

public class OrderController : Controller
{
    private readonly INotificationService _nofitificationService;

    public OrderController(INotificationService nofitificationService)
    {
        _nofitificationService = nofitificationService;
    }

    [HttpPost("place-order")]
    public IActionResult PlaceOrder(
       [FromServices] ECommerceDBContext dbContext,
       [FromServices] IUserService userService,
       [FromServices] IOrderService orderService,
       [FromServices] IFileService fileService)
       
    {
        var order = new Order
        {
            Status = OrderStatus.Created,
            TrackingCode = orderService.GenerateTrackingCode(),
            UserId = userService.CurrentUser.Id,
        };

     
        decimal total = 0;
        var orderItems = new List<OrderItem>();

      

        order.OrderItems = orderItems;

        _nofitificationService.SendOrderNotification(order);

        dbContext.Orders.Add(order);
        dbContext.SaveChanges();

        return RedirectToAction("orders", "account");
    }

}

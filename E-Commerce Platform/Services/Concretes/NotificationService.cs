using E_Commerce_Platform.Contracts;
using E_Commerce_Platform.DataBase;
using E_Commerce_Platform.DataBase.Models;
using E_Commerce_Platform.Hubs;
using E_Commerce_Platform.Services.Abstracts;
using Microsoft.AspNetCore.SignalR;
using System.Text;

namespace E_Commerce_Platform.Services.Concretes;

public class NotificationService : INotificationService
{

    private readonly IEmailService _emailService;
    private readonly ECommerceDBContext _dbContext;
    private readonly IUserService _userService;
    private readonly IHubContext<AlertMessageHub> _hubContext;
    private readonly IAlertMessageService _aletMessageService;

    public NotificationService(
        IEmailService emailService,
        ECommerceDBContext dbContext, 
        IUserService userService,
        IHubContext<AlertMessageHub> hubContext,
        IAlertMessageService aletMessageService)
    {
        _emailService = emailService;
        _dbContext = dbContext;
        _userService = userService;
        _hubContext = hubContext;
        _aletMessageService = aletMessageService;
    }

    public void CreateAndPushAlerMessage(List<User> recievers, string title, string content)
    {
        throw new NotImplementedException();
    }

    public void CreateAndPushAlerMessage(User reciever, string title, string content)
    {
        throw new NotImplementedException();
    }

    public void SendOrderApprovedNotification(Order order)
    {
        throw new NotImplementedException();
    }

    public void SendOrderCompletedNotification(Order order)
    {
        throw new NotImplementedException();
    }

    private string PrepareAlertMessageContent(Order order, string template)
    {
        var templayeBuilder = new StringBuilder(template)
           .Replace("{order_number}", order.TrackingCode);

        return templayeBuilder.ToString();
    }
    private string PrepareEmailMessageContent(Order order, string template)
    {
        var templayeBuilder = new StringBuilder(template)
            .Replace("{order_number}", order.TrackingCode)
            .Replace("{firstName}", order.User.Name)
            .Replace("{lastName}", order.User.LastName);

        return templayeBuilder.ToString();
    }

    public void SendOrderNotification(Order order)
    {
        switch (order.Status)
        {
            case OrderStatus.Created:
                SendOrderCreatedNotification(order);
                break;
            case OrderStatus.Approved:
                SendOrderApprovedNotification(order);
                break;
            case OrderStatus.Rejected:
                SendOrderRejectedNotification(order);
                break;
            case OrderStatus.Sent:
                SendOrderSentNotification(order);
                break;
            case OrderStatus.Completed:
                SendOrderCompletedNotification(order);
                break;
            default:
                throw new NotificationNotImplementedException();
        }
    }


    public void SendOrderCreatedNotification(Order order)
    {
        var emailMessageContent = PrepareAlertMessageContent(order, AlertMessageTemplates.Order.CREATED);
        var staffMembers = _userService.GetAllStaffMembers();
        CreateAndPustOrderAlertMessage(staffMembers, emailMessageContent);
    }

    public void SendOrderRejectedNotification(Order order)
    {
        throw new NotImplementedException();
    }

    public void SendOrderSentNotification(Order order)
    {
        throw new NotImplementedException();
    }
}

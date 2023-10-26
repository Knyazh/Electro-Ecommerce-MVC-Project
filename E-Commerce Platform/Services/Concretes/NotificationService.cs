using E_Commerce_Platform.Contracts;
using E_Commerce_Platform.DataBase;
using E_Commerce_Platform.DataBase.Models;
using E_Commerce_Platform.Exceptions;
using E_Commerce_Platform.Hubs;
using E_Commerce_Platform.Services.Abstracts;
using E_Commerce_Platform.ViewModels.User;
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

    public void SendOrderNotification(Order order)
    {
        switch (order.Status)
        {
            //case OrderStatus.Created:
            //    SendOrderCreatedNotification(order);
            //    break;
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

    #region Common

    public void CreateAndPushAlertMessage(List<User> receivers, string title, string content)
    {
        foreach (var receiver in receivers)
        {
            var alertMessage = new AlertMessage
            {
                Title = title,
                Content = content,
                UserId = receiver.Id
            };

            _dbContext.AlertMessages.Add(alertMessage);

            var connectIds = _aletMessageService.GetConnectionIds(receiver);

            var alerMessageViewModel = new AlertMessageViewModel
            {
                Title = alertMessage.Title,
                Content = alertMessage.Content,
                CreatedAt = DateTime.Now
            };

            _hubContext.Clients
                .Clients(connectIds)
                .SendAsync("ReceiveAlertMessage", alerMessageViewModel)
                .Wait();
        }
    }
    public void CreateAndPushAlertMessage(User receiver, string title, string content)
    {
        CreateAndPushAlertMessage(new List<User> { receiver }, title, content);
    }

    private void CreateAndPustOrderAlertMessage(List<User> receivers, string content)
    {
        CreateAndPushAlertMessage(receivers, AlertMessageTemplates.Order.TITLE, content);
    }
    private void CreateAndPustOrderAlertMessage(User receiver, string content)
    {
        CreateAndPushAlertMessage(receiver, AlertMessageTemplates.Order.TITLE, content);
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

    #endregion


    //public void SendOrderCreatedNotification(Order order)
    //{
    //    var emailMessageContent = PrepareAlertMessageContent(order, AlertMessageTemplates.Order.CREATED);
    //    var staffMembers = _userService.GetAllStaffMembers();
    //    CreateAndPustOrderAlertMessage(staffMembers, emailMessageContent);
    //}
    public void SendOrderApprovedNotification(Order order)
    {
        var emailMessageContent = PrepareEmailMessageContent(order, EmailTemplates.Order.APPROVED);
        _emailService.SendEmail(EmailTemplates.Order.SUBJECT, emailMessageContent, order.User.Email);

        var alertMessageContent = PrepareAlertMessageContent(order, AlertMessageTemplates.Order.APPROVED);
        CreateAndPustOrderAlertMessage(order.User, alertMessageContent);
    }
    public void SendOrderCompletedNotification(Order order)
    {
        var emailMessageContent = PrepareEmailMessageContent(order, EmailTemplates.Order.COMPLETED);
        _emailService.SendEmail(EmailTemplates.Order.SUBJECT, emailMessageContent, order.User.Email);

        var alertMessageContent = PrepareAlertMessageContent(order, AlertMessageTemplates.Order.COMPLETED);
        CreateAndPustOrderAlertMessage(order.User, alertMessageContent);
    }
    public void SendOrderRejectedNotification(Order order)
    {
        var emailMessageContent = PrepareEmailMessageContent(order, EmailTemplates.Order.REJECTED);
        _emailService.SendEmail(EmailTemplates.Order.SUBJECT, emailMessageContent, order.User.Email);

        var alertMessageContent = PrepareAlertMessageContent(order, AlertMessageTemplates.Order.REJECTED);
        CreateAndPustOrderAlertMessage(order.User, alertMessageContent);
    }
    public void SendOrderSentNotification(Order order)
    {
        var emailMessageContent = PrepareEmailMessageContent(order, EmailTemplates.Order.SENT);
        _emailService.SendEmail(EmailTemplates.Order.SUBJECT, emailMessageContent, order.User.Email);

        var alertMessageContent = PrepareAlertMessageContent(order, AlertMessageTemplates.Order.SENT);
        CreateAndPustOrderAlertMessage(order.User, alertMessageContent);
    }
}

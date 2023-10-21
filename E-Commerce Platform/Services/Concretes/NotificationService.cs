using E_Commerce_Platform.DataBase.Models;
using E_Commerce_Platform.Services.Abstracts;

namespace E_Commerce_Platform.Services.Concretes;

public class NotificationService : INotificationService
{
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

    public void SendOrderNotification(Order order)
    {
        throw new NotImplementedException();
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

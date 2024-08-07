﻿using E_Commerce_Platform.DataBase.Models;

namespace E_Commerce_Platform.Services.Abstracts;

public interface INotificationService
{

    void CreateAndPushAlertMessage(List<User> recievers, string title, string content);
    void CreateAndPushAlertMessage(User reciever, string title, string content);
    void SendOrderNotification(Order order);
    public void SendOrderApprovedNotification(Order order);
    public void SendOrderRejectedNotification(Order order);
    public void SendOrderSentNotification(Order order);
    public void SendOrderCompletedNotification(Order order);
}

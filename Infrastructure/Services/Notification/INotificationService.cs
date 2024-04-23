using Domain.Models.Notification;
using System;

namespace Infrastructure.Services.Notification
{
    public interface INotificationService
    {
        Task SendNotification(NotificationModel notification);
    }
}
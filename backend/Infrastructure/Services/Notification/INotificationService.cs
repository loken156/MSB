using Domain.Models.Notification;

namespace Infrastructure.Services.Notification
{
    public interface INotificationService
    {
        Task SendNotification(NotificationModel notification);
    }
}
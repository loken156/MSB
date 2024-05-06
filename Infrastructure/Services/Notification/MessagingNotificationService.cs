using Domain.Models.Notification;

namespace Infrastructure.Services.Notification
{
    public class MessagingNotificationService : INotificationService
    {
        private readonly IMessageSender _messageSender; // Implement IMessageSender according to your messaging service

        public MessagingNotificationService(IMessageSender messageSender)
        {
            _messageSender = messageSender;
        }

        public async Task SendNotification(NotificationModel notification)
        {
            // Send notification via messaging service
            await _messageSender.SendMessage(notification.UserId, notification.Message);
        }
    }
}
using Domain.Models.Notification;

namespace Infrastructure.Services.Notification
{
    public class MessagingNotificationService : INotificationService
    {
        private readonly IMessageSender _messageSender;

        public MessagingNotificationService(IMessageSender messageSender)
        {
            _messageSender = messageSender;
        }

        public async Task SendNotification(NotificationModel notification)
        {
            if (string.IsNullOrEmpty(notification.Email))
            {
                throw new ArgumentNullException(nameof(notification.Email), "Recipient email address is required.");
            }

            // Send notification via messaging service
            await _messageSender.SendMessage(notification.Email, notification.Message);
        }
    }
}
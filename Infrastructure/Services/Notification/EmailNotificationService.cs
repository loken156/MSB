using Domain.Models.Notification;
using System.Net;
using System.Net.Mail;
namespace Infrastructure.Services.Notification
{
    public class EmailNotificationService : INotificationService, IMessageSender
    {
        private readonly SmtpClient _smtpClient;
        private readonly string _senderEmail;
        private readonly string _senderPassword;

        public EmailNotificationService(string smtpServer, int port, string senderEmail, string senderPassword)
        {
            _smtpClient = new SmtpClient(smtpServer, port);
            _senderEmail = senderEmail;
            _senderPassword = senderPassword;
            _smtpClient.EnableSsl = true; // Enable SSL for secure connection
            _smtpClient.Credentials = new NetworkCredential(_senderEmail, _senderPassword);
        }

        public async Task SendNotification(NotificationModel notification)
        {
            await SendMessage(notification.UserId, notification.Message);
        }

        public async Task SendMessage(string userId, string message)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_senderEmail),
                Subject = "Order Notification",
                Body = message,
                IsBodyHtml = true
            };

            mailMessage.To.Add(userId);

            await _smtpClient.SendMailAsync(mailMessage);
        }
    }

}
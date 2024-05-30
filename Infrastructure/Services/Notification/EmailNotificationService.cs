using Domain.Models.Notification;
using System.Net;
using System.Net.Mail;

// This class implements the INotificationService and IMessageSender interfaces to provide functionality for sending email notifications.
// It utilizes the System.Net.Mail namespace for sending emails via SMTP (Simple Mail Transfer Protocol).
// The constructor initializes the SmtpClient with the provided SMTP server, port, sender email, and sender password.
// It sets up SSL for a secure connection and sets credentials for the sender email.
// The SendNotification method asynchronously sends a notification using the SendMessage method.
// The SendMessage method constructs a MailMessage with the sender email, subject, message body, and recipient email (userId).
// It then sends the email asynchronously using the configured SmtpClient.

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
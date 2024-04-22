using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Domain.Models.Notification;
namespace Infrastructure.Services.Notification
{
    public class EmailNotificationService : INotificationService
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
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_senderEmail),
                Subject = "Order Notification",
                Body = notification.Message,
                IsBodyHtml = true
            };

            mailMessage.To.Add(notification.UserId);

            await _smtpClient.SendMailAsync(mailMessage);
        }

    }
}



using Domain.Interfaces;
namespace Domain.Models.Notification
{
    public class NotificationModel
    {
        public string? UserId { get; set; }
        public string? Message { get; set; }
        public bool IsRead { get; set; }
    }
}
namespace Domain.Models.Notification
{
    public class NotificationModel
    {
        public string? UserId { get; set; }    // User ID to associate the notification with a user in your system
        public string? Email { get; set; }     // Email address for sending notifications
        public string? Message { get; set; }   // The message content
        public bool IsRead { get; set; }       // To track whether the notification has been read
    }
}
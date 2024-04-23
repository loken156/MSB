namespace Infrastructure.Services.Notification
{
    public interface IMessageSender
    {
        Task SendMessage(string userId, string message);
    }
}
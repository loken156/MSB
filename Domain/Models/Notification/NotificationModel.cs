﻿using Domain.Interfaces;
namespace Domain.Models.Notification
{
    public class NotificationModel
	{
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
    }
}


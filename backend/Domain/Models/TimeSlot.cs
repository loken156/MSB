using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class TimeSlot
    {
        [Key]
        public Guid TimeId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
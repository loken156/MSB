using System;

namespace Application.Dto.TimeSlot
{
    public class TimeSlotDto
    {
        public Guid Id { get; set; }         // Unique identifier

        private DateTime _date;
        
        // Getter and setter will still ensure no time component is saved
        public DateTime Date
        {
            get => _date.Date; // Only returns the date part
            set => _date = value.Date; // Sets only the date part
        }

        public string TimeSlot { get; set; }  // Time slot description

        public int Occupancy { get; set; }    // Number of slots occupied
    }
}
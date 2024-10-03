using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Domain.Models.TimeSlot
{
    public class TimeSlotModel
    {
        [Key]
        public Guid Id { get; set; } // Unique identifier

        [Required]
        public DateTime Date { get; set; } // Date of the time slot (year/month/day)

        [Required]
        [MaxLength(50)]
        public string TimeSlot { get; set; } // Time slot description

        [Required]
        public int Occupancy { get; set; } // Number of slots occupied

        // Navigation property: A TimeSlot can relate to multiple other entities if needed
        // public ICollection<OtherModel> OtherEntities { get; set; } // Example of a navigation property

        // Constructor to initialize properties
        public TimeSlotModel(Guid id, DateTime date, string timeSlot, int occupancy)
        {
            Id = id;
            Date = date;
            TimeSlot = timeSlot;
            Occupancy = occupancy;
        }

        // Default constructor for EF
        public TimeSlotModel()
        {
        }
    }
}
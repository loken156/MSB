using Domain.Models.Box;
using Domain.Models.BoxType;
using Domain.Models.Warehouse;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Shelf
{
    public class ShelfModel
    {
        [Key]
        public Guid ShelfId { get; set; }
        
        public char Section { get; set; }

        public int ShelfRows { get; set; }
        public int ShelfColumn { get; set; }

        public int LargeBoxCapacity { get; set; }  // Capacity for large boxes
        public int MediumBoxCapacity { get; set; } // Capacity for medium boxes
        public int SmallBoxCapacity { get; set; }  // Capacity for small boxes

        public int AvailableLargeSlots { get; set; }
        public int AvailableMediumSlots { get; set; }
        public int AvailableSmallSlots { get; set; }
        public bool Occupancy { get; set; }

        [ForeignKey("Warehouse")]
        public Guid WarehouseId { get; set; }
        public virtual WarehouseModel Warehouse { get; set; }

        // Relationship to Boxes
        public virtual ICollection<BoxModel> Boxes { get; set; } = new List<BoxModel>();
        
        // Optional relationship to BoxTypes (if needed)
        public ICollection<BoxTypeModel> BoxTypes { get; set; } = new List<BoxTypeModel>();
        
        // Generate available slots based on capacity
        public void InitializeAvailableSlots()
        {
            AvailableLargeSlots = LargeBoxCapacity;
            AvailableMediumSlots = MediumBoxCapacity;
            AvailableSmallSlots = SmallBoxCapacity;
        }
    }
}
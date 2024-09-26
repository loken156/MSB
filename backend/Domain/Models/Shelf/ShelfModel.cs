using Domain.Models.Box;
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
        // New columns for box capacities
        public int LargeBoxCapacity { get; set; }  // Capacity for large boxes
        public int MediumBoxCapacity { get; set; } // Capacity for medium boxes
        public int SmallBoxCapacity { get; set; }  // Capacity for small boxes
        // Available slots (handled by command handlers)
        public int AvailableLargeSlots { get; set; }
        public int AvailableMediumSlots { get; set; }
        public int AvailableSmallSlots { get; set; }
        public bool Occupancy { get; set; } // Indicates if the shelf is occupied

        // ForeignKey and Navigation Property for Warehouse
        [ForeignKey("Warehouse")]
        public Guid WarehouseId { get; set; }
        public virtual WarehouseModel Warehouse { get; set; }

        // Relationship to Boxes
        // Assuming a Shelf can have multiple Boxes
        public virtual ICollection<BoxModel> Boxes { get; set; } = new List<BoxModel>();
    }
}
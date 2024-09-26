using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Shelf
{
    public class ShelfDto
    {
        [Required] public Guid ShelfId { get; set; }
        
        public char Section { get; set; }

        [Required] public int ShelfRows { get; set; }
        [Required] public int ShelfColumn { get; set; }
        [Required] public int LargeBoxCapacity { get; set; }
        [Required] public int MediumBoxCapacity { get; set; }
        [Required] public int SmallBoxCapacity { get; set; }
        public int AvailableLargeSlots { get; set; }
        public int AvailableMediumSlots { get; set; }
        public int AvailableSmallSlots { get; set; }
        [Required] public bool Occupancy { get; set; }
        [Required] public string WarehouseName { get; set; }
        public Guid WarehouseId { get; set; }

    }
}
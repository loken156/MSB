using System.ComponentModel.DataAnnotations;

namespace Application.Dto.AddShelf
{
    public class AddShelfDto
    {
        [Required] public Guid ShelfId { get; set; }
        [Required] public char Section { get; set; }
        [Required] public int ShelfRows { get; set; }
        [Required] public int ShelfColumn { get; set; }
        [Required] public int LargeBoxCapacity { get; set; }
        [Required] public int MediumBoxCapacity { get; set; }
        [Required] public int SmallBoxCapacity { get; set; }
        [Required] public bool Occupancy { get; set; }
        [Required] public string WarehouseName { get; set; }

    }
}
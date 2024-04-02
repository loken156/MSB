using System.ComponentModel.DataAnnotations;

namespace Application.Dto.AddShelf
{
    public class AddShelfToNewWarehouseDto
    {
        [Required] public int ShelfRow { get; set; }
        [Required] public int ShelfColumn { get; set; }
        [Required] public bool Occupancy { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Warehouse
{
    public class WarehouseDto
    {
        [Required] public Guid WarehouseId { get; set; }
        [Required] public string WarehouseName { get; set; }
        [Required] public Guid AddressId { get; set; }
        [Required] public Guid ShelfId { get; set; }
    }
}

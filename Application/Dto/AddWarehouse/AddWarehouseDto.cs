using System.ComponentModel.DataAnnotations;

namespace Application.Dto.AddWarehouse
{
    public class AddWarehouseDto
    {
        [Required] public string WarehouseName { get; set; }
        [Required] public Guid AddressId { get; set; }
        [Required] public Guid ShelfId { get; set; }
    }
}
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.AddWarehouse
{
    public class AddWarehouseDto
    {
        [Required] public string WarehouseName { get; set; }
        [Required] 
        [DefaultValue("Need existing Guid")]
        public Guid AddressId { get; set; }
        //[Required] public ICollection<Guid>? ShelfIds { get; set; }
    }
}
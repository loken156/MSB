using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Order
{
    public class AddOrderDto
    {

        [Required] public Guid OrderId { get; set; }
        [Required] public int OrderNumber { get; set; }
        [Required] public DateTime OrderDate { get; set; }
        [Required] public string UserId { get; set; }
        [Required] public Guid AddressId { get; set; }
        public List<AddBoxToOrderDto> Boxes { get; set; } = new List<AddBoxToOrderDto>();
        public string RepairNotes { get; set; } = "No notes";

    }
}
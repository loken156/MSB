using Application.Dto.Box;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Order
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        [Required] public int OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        [Required] public decimal TotalCost { get; set; }
        public string? OrderStatus { get; set; }
        [Required] public string UserId { get; set; }
        public Guid? CarId { get; set; }
        //public Guid RepairId { get; set; }
        //[Required] public Guid WarehouseId { get; set; }
        [Required] public Guid AddressId { get; set; }
        public List<BoxDto>? Boxes { get; set; } = new List<BoxDto>();
        public string RepairNotes { get; set; } = "No notes";
    }
}
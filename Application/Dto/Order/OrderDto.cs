using Application.Dto.User;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Order
{
    public class OrderDto
    {
        [Required] public Guid OrderId { get; set; }
        [Required] public int OrderNumber { get; set; }
        [Required] public DateTime OrderDate { get; set; }
        [Required] public decimal TotalCost { get; set; }
        public string? OrderStatus { get; set; }
        [Required] public string UserId { get; set; }
        public Guid? CarId { get; set; }
        //public Guid RepairId { get; set; }
        [Required] public Guid WarehouseId { get; set; }
        [Required] public Guid AdressId { get; set; }
        public string RepairNotes { get; set; } = "No notes";
    }
}
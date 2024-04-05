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
        [Required] public Guid UserId { get; set; }
        public User.UserDto User { get; set; }
        [Required] public Guid CarId { get; set; } 
        public Guid RepairId { get; set; }
        [Required] public Guid WarehouseId { get; set; }
        // [Required] public int AdressId { get; set; } // Commented out to avoid error
        public string RepairNotes { get; set; } = "No notes";
    }
}

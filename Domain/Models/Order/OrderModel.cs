using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Order
{
    public class OrderModel
    {
        [Key]
        public Guid OrderId { get; set; }
        public int OrderNumber { get; set; }

        public DateTime OrderDate { get; set; }
        public decimal TotalCost { get; set; }
        public string OrderStatus { get; set; } = string.Empty;
        [ForeignKey("UserId")]
        public string UserId { get; set; }

        [ForeignKey("CarId")]
        public Guid CarId { get; set; }
        public Car.CarModel Car { get; set; }

        //[ForeignKey("RepairId")]
        //public int RepairId { get; set; }
        //public Repair.RepairDto Repair { get; set; }

        [ForeignKey("WarehouseId")]
        public Guid WarehouseId { get; set; }
        public Warehouse.WarehouseModel Warehouse { get; set; }

        [ForeignKey("AdressId")] // Return Address
        public int AdressId { get; set; }
        public Address.AddressModel Address { get; set; }

        public string RepairNotes { get; set; } = "No notes";

    }
}
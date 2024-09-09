using Domain.Models.Box;
using Domain.Models.Warehouse;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Shelf
{
    public class ShelfModel
    {
        [Key]
        public Guid ShelfId { get; set; }

        public int ShelfRow { get; set; }
        public int ShelfColumn { get; set; }
        public bool Occupancy { get; set; } // Indicates if the shelf is occupied

        // ForeignKey and Navigation Property for Warehouse
        [ForeignKey("Warehouse")]
        public Guid WarehouseId { get; set; }
        public virtual WarehouseModel Warehouse { get; set; }

        // Relationship to Boxes
        // Assuming a Shelf can have multiple Boxes
        public virtual ICollection<BoxModel> Boxes { get; set; } = new List<BoxModel>();
    }
}
using Domain.Models.BoxType;
using Domain.Models.Order;
using Domain.Models.Shelf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Box
{
    public class BoxModel
    {
        [Key]
        public Guid BoxId { get; set; }

        public string Type { get; set; } = string.Empty;

        public int TimesUsed { get; set; }

        public int Stock { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public string UserNotes { get; set; } = string.Empty;

        [ForeignKey("OrderId")]
        public Guid OrderId { get; set; }
        public virtual OrderModel Order { get; set; }

        // Add the Size property back to the BoxModel
        public string Size { get; set; } = string.Empty; // Size of the box

        [ForeignKey("ShelfId")]
        public Guid ShelfId { get; set; }
        public virtual ShelfModel Shelf { get; set; }

        // Foreign key to BoxType
        public int BoxTypeId { get; set; }
        public virtual BoxTypeModel BoxType { get; set; }  // Navigation property to BoxType
    }
}
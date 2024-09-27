using Application.Dto.BoxType;
using Domain.Models.Order;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Dto.Box
{
    public class BoxDto
    {
        public Guid BoxId { get; set; }
        public string Type { get; set; }
        public int TimesUsed { get; set; }
        public int Stock { get; set; }
        public string ImageUrl { get; set; }
        public string? UserNotes { get; set; }
        public Guid ShelfId { get; set; }
        public Guid OrderId { get; set; }

        // Reference to BoxTypeDto for size
        public BoxTypeDto BoxType { get; set; } // This will contain the Size property
        public string Size { get; set; }
    }
}

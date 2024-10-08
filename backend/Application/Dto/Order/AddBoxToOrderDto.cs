namespace Application.Dto.Order
{
    public class AddBoxToOrderDto
    {
        public Guid BoxId { get; set; }
        public Guid? OrderId { get; set; } // Nullable, to optionally link to an order
        public string Type { get; set; }  // Box type, assuming from BoxType
        public string ImageUrl { get; set; } // Image of the box
        public string? UserNotes { get; set; } // Optional user notes
        public string Size { get; set; } // Box size, inferred from BoxType (Small, Medium, Large, etc.)
    }
}
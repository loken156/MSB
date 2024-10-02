using Application.Dto.BoxType;

namespace Application.Dto.Box
{
    public class BoxDto
    {
        public Guid BoxId { get; set; }
        public int TimesUsed { get; set; }
        public int Stock { get; set; }
        public string ImageUrl { get; set; }
        public string? UserNotes { get; set; }
        public Guid? OrderId { get; set; }

        public int BoxTypeId { get; set; } // Store the BoxTypeId for the box type

        // This will be mapped from BoxType
        public string Size { get; set; } // Derived from BoxType
        public string Type { get; set; } // Derived from BoxType
    }
}
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
        public Guid? OrderId { get; set; } // Optional OrderId for linking the box to an order
        public int BoxTypeId { get; set; }  // Only pass BoxTypeId, not the full BoxType object

        // Size is derived from BoxType, no need to pass it from the client.
        // It will be fetched automatically from the database when the BoxType is retrieved.
        public string Size { get; set; } 
    }
}
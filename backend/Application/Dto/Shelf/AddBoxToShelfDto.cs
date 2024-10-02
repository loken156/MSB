namespace Application.Dto.Shelf
{
    public class AddBoxToShelfDto
    {
        public Guid BoxId { get; set; }   // ID of the box to be added
        public Guid ShelfId { get; set; } // ID of the shelf to add the box to
    }
}
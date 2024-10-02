using System.ComponentModel.DataAnnotations;

namespace Application.Dto.BoxType
{
    public class BoxTypeDto
    {
        public int BoxTypeId { get; set; }  // Unique identifier
        
        public string Type { get; set; }

        public string Size { get; set; }    // Size of the box (immutable)

        public int Stock { get; set; }      // Amount (modifiable)

        public string Description { get; set; }  // Description of the box (immutable)
    }
}
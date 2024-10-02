using Domain.Models.Box;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.BoxType
{
    public class BoxTypeModel   
    {
        [Key]
        public int BoxTypeId { get; set; } // Unique identifier

        [Required]
        [MaxLength(50)]
        public string Size { get; private set; } // Size of the box (immutable)
        
        public string Type { get; set; } = string.Empty;  // Type of the box (Cardboard, Plastic, etc.)

        [Required]
        public int Stock { get; set; } // Amount (modifiable)

        [MaxLength(200)]
        public string Description { get; private set; } // Description of the box (immutable)
        
        // Navigation property: A BoxType can have many Boxes
        public ICollection<BoxModel> Boxes { get; set; }
    
        // Constructor to ensure immutability of size and description
        public BoxTypeModel(string size, int stock, string description)
        {
            Size = size;
            Stock = stock;
            Description = description;
            Boxes = new List<BoxModel>(); // Ensure it's initialized
        }

        // Default constructor for EF
        public BoxTypeModel()
        {
            Boxes = new List<BoxModel>(); // Ensure it's initialized
        }

        // Method to update stock
        public void UpdateStock(int changeInStock)
        {
            Stock += changeInStock;  // Logic to update stock
        }
    }
}
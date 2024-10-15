using Application.Dto.Box;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Order
{
    public class AddOrderDto
    {
        public Guid? OrderId { get; set; } // Nullable, auto-generated if not provided
        public int OrderNumber { get; set; } // Auto-generated server-side
        public DateTime OrderDate { get; set; } = DateTime.UtcNow; // Default to current date
        public string UserId { get; set; } // Required for linking to a user
        public Guid? AddressId { get; set; } // Nullable, can be assigned later
        public string RepairNotes { get; set; } = "No notes"; // Default value

        // Optional list of boxes that the customer can add to the order
        public List<BoxDto>? Boxes { get; set; }
        
        public string Address { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string DeliverToName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
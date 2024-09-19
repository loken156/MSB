// This code defines the OrderModel class within the Domain.Models.Order namespace, 
// which represents an order in the application's domain layer. 
// This class includes properties for order details, relationships with other entities, and relevant data annotations.

using Domain.Models.Box; // Importing the Box model from the Domain.Models.Box namespace.
using System.ComponentModel.DataAnnotations; // Importing data annotation attributes.
using System.ComponentModel.DataAnnotations.Schema; // Importing attributes for database schema mapping.
using System.Text.Json.Serialization; // Importing attributes for JSON serialization control.

namespace Domain.Models.Order
{
    public class OrderModel
    {
        // Primary key for the OrderModel.
        [Key]
        public Guid OrderId { get; set; }

        // Unique order number.
        public int OrderNumber { get; set; }

        // Date when the order was placed.
        public DateTime OrderDate { get; set; }

        // Total cost of the order.
        public decimal TotalCost { get; set; }

        // Current status of the order.
        public string OrderStatus { get; set; } = string.Empty;

        // Foreign key linking to the user who placed the order.
        [ForeignKey("UserId")]
        public string UserId { get; set; }

        // Foreign key linking to the car associated with the order, if any.
        // JsonIgnore attribute prevents this property from being serialized into JSON, avoiding circular references.
        [ForeignKey("CarId")]
        [JsonIgnore]
        public Guid? CarId { get; set; }

        // Navigation property for the car associated with the order.
        // JsonIgnore attribute prevents this property from being serialized into JSON.
        [JsonIgnore]
        public Car.CarModel? Car { get; set; }

        // Foreign key linking to the return box associated with the order.
        // JsonIgnore attribute prevents this property from being serialized into JSON.
        //[ForeignKey("BoxId")]
        //[JsonIgnore]
        //public Guid? BoxId { get; set; }

        // Foreign key linking to the return address associated with the order.
        [ForeignKey("AddressId")]
        public Guid AddressId { get; set; }

        // Navigation property for the address associated with the order.
        public Address.AddressModel Address { get; set; }

        // Notes about the repair, with a default value of "No notes".
        public string RepairNotes { get; set; } = "No notes";

        // Collection of boxes associated with the order.
        // Initialized to an empty list to avoid null reference exceptions.
        public ICollection<BoxModel>? Boxes { get; set; } = new List<BoxModel>();
    }
}
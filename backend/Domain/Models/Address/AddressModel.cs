using Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models.Address
{
    public class AddressModel
    {
        [Key]
        public Guid AddressId { get; set; }
        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public string StreetName { get; set; } = string.Empty;
        
        public string UnitNumber { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string FullAddress { get; set; } = string.Empty;
        
        public string City { get; set; } = "Singapore";
        public string State { get; set; } = "Singapore";
        public string Country { get; set; } = "Singapore";
        
        
    }
               
}
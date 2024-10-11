using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Adress
{
    public class AddressDto
    {
        public Guid AddressId { get; set; }
        public string? UserId { get; set; }
        
        [Required]
        public string? StreetName { get; set; }
       
        /// <summary>
        /// Example #01-01
        /// </summary>
        public string UnitNumber { get; set; } 
        [Required] 
        public string? ZipCode { get; set; }
        
        
    }
}
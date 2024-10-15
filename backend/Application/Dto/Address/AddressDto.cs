using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Application.Dto.Adress
{
    public class AddressDto
    {
        public Guid AddressId { get; set; }
        
        [DefaultValue("Auto Generated")]
        public string? UserId { get; set; }
        
        [Required]
        [DefaultValue("Any String")]
        public string? StreetName { get; set; }    
        
        [DefaultValue("#12-34")]
        public string UnitNumber { get; set; } 
        
        [Required]      
        [DefaultValue("123456")]
        public string? ZipCode { get; set; }
        
        
    }
}
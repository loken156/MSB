using Application.Dto.Adress;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Register
{
    public class RegisterDto
    {
        [Required]
        [DefaultValue("abc@gmail.com")]
        public string Email { get; set; }

        [Required]
        [SwaggerSchema(Description = "At least one big letter/small letter/number/symbol. Min 7 symbols")]
        [DefaultValue("123Abc!")]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [DefaultValue("12345678")]
        public string PhoneNumber { get; set; }

        [Required]
        public AddressDto Address { get; set; }
    }
}
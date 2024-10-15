using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Admin
{
    public class AdminDto
    {
        [Required] public Guid AdminId { get; set; }
        [Required] 
        [DefaultValue("xxx@mail.xx")]
        public string Email { get; set; } = string.Empty;
        [Required]
        [SwaggerSchema(Description = "At least one big letter/small letter/number/symbol. Min 7 symbols")]
        [DefaultValue("123Abc!")]
        public string Password { get; set; } = string.Empty;
        [Required] public string FirstName { get; set; } = string.Empty;
        [Required] public string LastName { get; set; } = string.Empty;
        [Required] public List<string> Roles { get; set; } = new List<string>();
        // [Required] public IList<string> Permissions { get; set; } = new List<string>();
    }
}
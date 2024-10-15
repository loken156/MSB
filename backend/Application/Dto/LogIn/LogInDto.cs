using System.ComponentModel;

namespace Application.Dto.LogIn
{
    public class LogInDto
    {
        public string Email { get; set; }
        
        [DefaultValue("123Abc!")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
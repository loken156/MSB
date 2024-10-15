using Application.Dto.Adress;
using System.ComponentModel;

namespace Application.Dto.UpdateUserInfo
{
    public class UpdateUserInfoDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DefaultValue("469065234")]
        public string PhoneNumber { get; set; }

        public AddressDto Address { get; set; }
    }
}
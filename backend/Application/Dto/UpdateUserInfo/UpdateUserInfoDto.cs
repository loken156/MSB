using Application.Dto.Adress;

namespace Application.Dto.UpdateUserInfo
{
    public class UpdateUserInfoDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public AddressDto Address { get; set; }
    }
}
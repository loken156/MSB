namespace Application.Dto.Adress
{
    public class AddressDto
    {
        public Guid AddressId { get; set; }
        public string? UserId { get; set; }
        public string? StreetName { get; set; }
        public string? StreetNumber { get; set; }
        public string? Apartment { get; set; }
        public string? ZipCode { get; set; }
        public string? Floor { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
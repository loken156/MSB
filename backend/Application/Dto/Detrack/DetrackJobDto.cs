namespace Application.Dto.Detrack
{
    public class DetrackJobDto
    {
        public string DoNumber { get; set; }
        public string Type { get; set; } = "Delivery";  // Can be "Delivery" or "Collection"
        public string Address { get; set; }
        public string Date { get; set; }  // Format: "YYYY-MM-DD"
        public string OrderNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string DeliverTo { get; set; } // Contact name
        public string PhoneNumber { get; set; }
        public string Instructions { get; set; }
        // Add more properties as per your requirements, following the Detrack API spec
    }
}